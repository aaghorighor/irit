namespace Suftnet.Cos.Web.Command
{
    using Common;
    using DataAccess;
    using Suftnet.Cos.Stripe;
    using System;
  
    public class CampaignChargeCommand : ICommand
    {
        private ICustomerProvider _customerProvider;
        private IChargeProvider _chargeProvider;
        private readonly IPledger _iPledger;
        private readonly IPledge _iPledge;
        private readonly ITenant _tenant;
        private readonly IPledgeCommand _pledgeCommand;
        public CampaignChargeCommand(IPledger pledger, IPledge pledge, 
            ITenant tenant, IPledgeCommand pledgeCommand)
        {
            _iPledger = pledger;          
            _iPledge = pledge;
            _tenant = tenant;
            _pledgeCommand = pledgeCommand;
        }

        public PledgerModel PledgerModel { get; set; }
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
                var pledge = CreatePledge();
                var _tenantModel = CreateTenant(pledge);
                var _stripeCustomerId = CreateCustomer(PledgerModel.StripeCustomerId);

                _customerProvider = new CustomerProvider(_tenantModel.StripeSecretKey);
                _chargeProvider = new ChargeProvider(_tenantModel.StripeSecretKey);

                if(string.IsNullOrEmpty(_stripeCustomerId))
                {
                    _stripeCustomerId = _customerProvider.Create(PledgerModel.Email);                   
                }

               var _charge = _chargeProvider.Charge((long)PledgerModel.Amount, 
                   ChargeCurrency.Pound, _stripeCustomerId, PledgerModel.SourceToken, $"{pledge.Title}", out error);

                if (_charge)
                {
                    Error = false;
                    CreateCampaign(_stripeCustomerId);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }  
                   
        }
        private void CreateCampaign(string stripeCustomerId)
        {
            var model = new PledgerDto
            {
                FirstName = PledgerModel.FirstName,
                LastName = PledgerModel.LastName,
                Email = PledgerModel.Email,
                Mobile = PledgerModel.Mobile,
                Note = PledgerModel.Note,
                Amount = PledgerModel.Amount,
                PledgeId = PledgerModel.CampaignId,
                Reference = stripeCustomerId,

                CreatedBy = PledgerModel.Email,
                CreatedDT = DateTime.UtcNow
            };

            _iPledger.Insert(model);

            _pledgeCommand.PledgeId = model.PledgeId;
            _pledgeCommand.CreatedBy = model.CreatedBy;
            _pledgeCommand.CreatedDT = model.CreatedDT;
            _pledgeCommand.Execute();
        }     
        private string CreateCustomer(string stripeCustomerId)
        {
           return stripeCustomerId != null? _customerProvider.GetCustomerId(stripeCustomerId) : string.Empty;
        }
        private PledgeDto CreatePledge()
        {
            return _iPledge.Get(PledgerModel.CampaignId);
        }
        private TenantDto CreateTenant(PledgeDto pledge)
        {            
            var _tenantModel = _tenant.Get(pledge.TenantId);

            if (_tenantModel == null)
            {
                throw new Exception("Tenant not found");
            }

            return _tenantModel;
        }
    }
}