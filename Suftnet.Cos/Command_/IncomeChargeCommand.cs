namespace Suftnet.Cos.Web.Command
{

    using Common;
    using DataAccess;
    using Suftnet.Cos.Extension;
    using Suftnet.Cos.Stripe;
    using System;
    using ViewModel;

    public class IncomeChargeCommand  : ICommand
    {
        private ICustomerProvider _customerProvider;
        private IChargeProvider _chargeProvider;
        private readonly IIncome _income;
        private readonly ITenant _tenant;
        private readonly ITenantCommon _common;
        public IncomeChargeCommand(IIncome income, ITenant tenant, ITenantCommon common)
        {         
            _income = income;
            _tenant = tenant;
            _common = common;
        }

        public GivingModel GivingModel { get; set; }
        public bool Error { get; set; } = true;

        public void Execute()
        {
            Charge();
        }       

        private void Charge()
        {
            try
            {
                var error = string.Empty;
                var incomeType = PrepareIncomeType();
                var _tenantModel = PrepareTenant();
                var _stripeCustomerId = PrepareCustomer(GivingModel.StripeCustomerId);

                _customerProvider = new CustomerProvider(_tenantModel.StripeSecretKey);
                _chargeProvider = new ChargeProvider(_tenantModel.StripeSecretKey);

                if(string.IsNullOrEmpty(_stripeCustomerId))
                {
                    _stripeCustomerId = _customerProvider.Create(GivingModel.Email);                   
                }

               var _charge = _chargeProvider.Charge(GivingModel.Amount, 
                   ChargeCurrency.Pound, _stripeCustomerId, 
                   GivingModel.SourceToken, $"{incomeType.Title}",  out error);

                if (_charge)
                {
                    Error = false;
                    PrepareIncome(_stripeCustomerId, _tenantModel.TenantId);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }  
                   
        }

        private void PrepareIncome(string stripeCustomerId, int tenantId)
        {
            var incomeModel = new IncomeDto
            {
                Amount = GivingModel.Amount,
                IncomeTypeId = GivingModel.IncomeTypeId,
                StripeReference = stripeCustomerId,
                MemberReference = GivingModel.FirstName + " " + GivingModel.LastName,
                Note = GivingModel.Note,
                GiftAid = GivingModel.GiftAid,
                TenantId = tenantId,

                CreatedBy = GivingModel.Email,
                CreatedDT = DateTime.UtcNow
            };

            this.PrepareGiftAid(incomeModel);

            _income.Insert(incomeModel);

        }

        private TenantDto PrepareTenant()
        {
            var _tenantModel = _tenant.Get(GivingModel.ExternalId.ToDecrypt().ToInt());

            if(_tenantModel == null)
            {
                throw new Exception("Tenant not found");
            }

            return _tenantModel;           
        }

        private string PrepareCustomer(string stripeCustomerId)
        {
           return stripeCustomerId != null? _customerProvider.GetCustomerId(stripeCustomerId) : string.Empty;
        }
        private TenantCommonDto PrepareIncomeType()
        {
            return _common.Get(GivingModel.IncomeTypeId);
        }

        private void PrepareGiftAid(IncomeDto income)
        {
            var isGiftAid = income.GiftAid == null ? false : true;
            var __tenantModel = PrepareTenant();

            income.Total = income.Amount;
            income.GiftAidAmount = 0;

            if (isGiftAid && __tenantModel.GiftAidPercentage != null)
            {
                income.GiftAidAmount = (income.Amount * (decimal)__tenantModel.GiftAidPercentage) / 100;
                income.Total = income.GiftAidAmount + income.Amount;
            }          
        }
    }
}