namespace Suftnet.Cos.Web.Command
{
    using Common;
    using Suftnet.Cos.Core;
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
             
        public Guid TenantId { get; set; }
        public Guid AddressId { get; set; }
        public int PlanTypeId { get; set; }
        public string SubscriptionId { get; set; }
        public Guid StatusId { get; set; }
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
                PlanTypeId = TenantModel.PlanTypeId,
                ExpirationDate = ExpirationDate,
                StartDate = this.StartDate,
                CustomerStripeId =  string.Empty,
                AddressId = AddressId,
                Email = TenantModel.Email,
                Description = string.Empty,               
                LogoUrl = string.Empty,
                Telephone = string.Empty,
                Mobile = TenantModel.Mobile,
                Name = TenantModel.Name,             
                StatusId = StatusId,
                Startup = false,
                //IsExpired = TenantModel.PlanTypeId == PlanType.Trial ? false : true,
                IsExpired = false,
                Publish = false,
                SubscriptionId = 0,
                Id = Guid.NewGuid(),

                CurrencyId = Currency.Default,
                CurrencyCode = Constant.DefaultHexCurrencySymbol,
                    
                CreatedBy = CreatedBy,
                CreatedDT = DateTime.UtcNow
            };
            try
            {
                TenantId = _tenant.Insert(model);
            }
            catch (Exception ex)
            { GeneralConfiguration.Configuration.Logger.LogError(ex); }
           
        }

        #endregion

    }
}