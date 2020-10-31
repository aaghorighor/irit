namespace Suftnet.Cos.Web.Command
{
    using System;
    using Suftnet.Cos.Core;
    using Suftnet.Cos.DataAccess;
    using ViewModel;

    public class CreateAddressCommand : ICommand
    {
        private readonly ITenantAddress _address;
        public CreateAddressCommand(ITenantAddress address)
        {
            _address = address;          
        }

        public Guid AddressId { get; set; }
        public string CreatedBy { get; set; }
        public CheckoutModel AddressModel { get; set; }
        public void Execute()
        {
            this.PrepareTenantAddress();
        }

        #region private function

        private void PrepareTenantAddress()
        {
            var address = new TenantAddressDto
            {
                AddressLine1 = AddressModel.AddressLine1,
                AddressLine2 = AddressModel.AddressLine2,
                AddressLine3 = AddressModel.AddressLine3,
                CompleteAddress = AddressModel.CompleteAddress,
                Country = AddressModel.Country,
                County = AddressModel.County,
                Latitude = AddressModel.Latitude,
                Longitude = AddressModel.Logitude,
                PostCode = AddressModel.PostCode,
                Town = AddressModel.Town,
                Id = Guid.NewGuid(),
                
                CreatedBy = CreatedBy,
                CreatedDT = DateTime.UtcNow
            };

            try
            {
                AddressId = _address.Insert(address); }
            catch(Exception ex)
            { GeneralConfiguration.Configuration.Logger.LogError(ex); }           
        }

        #endregion
    }
}