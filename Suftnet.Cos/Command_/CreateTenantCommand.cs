namespace Suftnet.Cos.Web.Command
{
    using Common;
    using Suftnet.Cos.DataAccess;   
    using Suftnet.Cos.Web.ViewModel;
    using System;

    public class CreateTenantCommand : ICommand
    {
        private readonly ITenant _tenant;
        public CreateTenantCommand(ITenant tenant)
        {
            _tenant = tenant;                    
        }

        public int UserId { get; set; }
        public int TenantId { get; set; }
        public int AddressId { get; set; }
        public int PlanTypeId { get; set; }
        public string SubscriptionId { get; set; }
        public int StatusId { get; set; }
        public int CutOff { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime? StartDate { get; set; }
        public string CreatedBy { get; set; }
        public CheckoutModel TenantModel { get; set; }
        public void Execute()
        {
            this.CreateTenant();
        }

        #region private function

        private void CreateTenant()
        {
            var model = new TenantDto
            {
                DenominationId = TenantModel.DenominationId,             
                PlanTypeId = TenantModel.PlanTypeId,
                ExpirationDate = ExpirationDate,
                StartDate = this.StartDate,
                CustomerStripeId =  string.Empty,
                AddressId = AddressId,
                Email = TenantModel.Email,
                OurBelieve = string.Empty,
                WhoWeAre = string.Empty,
                LogoUrl = string.Empty,
                Telephone = string.Empty,
                Mobile = TenantModel.Mobile,
                Name = TenantModel.Name,             
                StatusId = StatusId,
                Startup = false,
                IsExpired = TenantModel.PlanTypeId == PlanType.Trial ? false : true,
                Publish = false,
                SubscriptionId = string.Empty ,
                SliderHomeOptionId = (int)DeliveryOptions.Auto,             
                CurrencyId = Currency.Default,
                CurrencyCode = Constant.DefaultHexCurrencySymbol,
                    
                CreatedBy = CreatedBy,
                CreatedDT = DateTime.UtcNow
            };

            TenantId = _tenant.Insert(model);
        }

        #endregion

    }
}