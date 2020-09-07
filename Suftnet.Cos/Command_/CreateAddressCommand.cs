namespace Suftnet.Cos.Web.Command
{
    using System;
    using Suftnet.Cos.DataAccess;
    using ViewModel;
    using Service;

    public class CreateAddressCommand : ICommand
    {
        private readonly IAddress _address;
        public CreateAddressCommand(IAddress address)
        {
            _address = address;          
        }

        public int AddressId { get; set; }
        public string CreatedBy { get; set; }
        public CheckoutModel AddressModel { get; set; }
        public void Execute()
        {
            this.PrepareTenantAddress();
        }

        #region private function

        private void PrepareTenantAddress()
        {
            var address = new AddressDto
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
                
                CreatedBy = CreatedBy,
                CreatedDT = DateTime.UtcNow
            };

            AddressId = _address.Insert(address);
        }

        #endregion
    }
}