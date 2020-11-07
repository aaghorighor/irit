namespace Suftnet.Cos.Web.Command
{
    using Common;
    using Suftnet.Cos.Core;
    using Suftnet.Cos.DataAccess;   
    using Suftnet.Cos.Web.ViewModel;
    using System;
    using System.Linq;
    using System.Security.Cryptography;

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
        public string AppCode { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime? StartDate { get; set; }
        public string CreatedBy { get; set; }
        public CheckoutModel CheckoutModel { get; set; }
        public void Execute()
        {
            this.CreateTenant();
        }

        #region private function

        private void CreateTenant()
        {
            var model = new TenantDto
            {
                PlanTypeId = CheckoutModel.PlanTypeId,
                ExpirationDate = ExpirationDate,
                StartDate = this.StartDate,
                CustomerStripeId = string.Empty,
                AddressId = AddressId,
                Email = CheckoutModel.Email,
                Description = string.Empty,
                LogoUrl = string.Empty,
                Telephone = string.Empty,
                Mobile = CheckoutModel.Mobile,
                Name = CheckoutModel.Name,
                StatusId = StatusId,
                Startup = false,
                IsFlatRate = false,
                DeliveryLimitNote = string.Empty,
                DeliveryRate = 0m,
                DeliveryUnitId = DeliveryUnit.Miles,
                FlatRate = 0m,
                //IsExpired = TenantModel.PlanTypeId == PlanType.Trial ? false : true,
                IsExpired = false,
                Publish = false,
                SubscriptionId = string.Empty,
                Id = Guid.NewGuid(),

                CurrencyId = Currency.Default,
                CurrencyCode = Constant.DefaultHexCurrencySymbol,
                    
                CreatedBy = CreatedBy,
                CreatedDT = DateTime.UtcNow
            };
            try
            {
                var appCode = GetHashCode(model.Id);
                model.AppCode = appCode.ToString();
                CheckoutModel.AppCode = appCode.ToString();
                this.TenantId = model.Id;

               _tenant.Insert(model);
            }
            catch (Exception ex)
            { GeneralConfiguration.Configuration.Logger.LogError(ex); }
           
        }

        private int GetHashCode(Guid guid)
        {
           return guid.GetHashCode();
        }

        private int CryptoServiceProvider(Guid guid)
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[8];
                randomNumberGenerator.GetBytes(randomNumber);

               return BitConverter.ToInt32(randomNumber, 0);               
            }
        }
        #endregion



    }
}