namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using ViewModel;    
    using System;
    using System.Threading.Tasks;
    using Suftnet.Cos.Services.Interface;
    
    public class BoostrapCommand : IBoostrapCommand
    {      
        private readonly ICategory _category;
        private readonly IMenu _menu;
        private readonly IAddon _addon;
        private readonly IJwToken _jwToken;
        private readonly IMobilePermission _mobilePermission;
        private readonly ITax _tax;
        private readonly IDiscount _discount;

        public BoostrapCommand(
            ICategory category, IAddon addon, IMobilePermission mobilePermission,
             IJwToken jwToken, IDiscount discount, ITax tax,
            IMenu menu)
        {
            _category = category;
            _menu = menu;
            _addon = addon;
            _jwToken = jwToken;
            _tax = tax;
            _discount = discount;
            _mobilePermission = mobilePermission;
        }               
              
        public Guid TenantId { get; set; }
        public MobileTenantDto User { get; set; }

        public async Task<BoostrapModel> Execute()
        {
            var task = await Task.Factory.StartNew(() => {

                var model = new BoostrapModel
                {
                    Menus = _menu.GetBy(TenantId),
                    Categories = _category.GetBy(TenantId),
                    Addons = _addon.GetBy(TenantId),
                    Taxes = _tax.Fetch(TenantId),
                    Discounts = _discount.Fetch(TenantId),
                    Outlet = new
                    {
                        user = new
                        {
                            firstName = User.FirstName,
                            lastName = User.LastName,                         
                            areaId = User.AreaId,
                            phoneNumber = User.PhoneNumber,
                            userName = User.UserName,
                            userId = User.Id,
                            externalId = User.TenantId,
                            permissions = GetUserPermissions(),
                            token = _jwToken.Create(User.UserName, User.Id)
                        },                       
                        tenant = new
                        {
                            name = User.Name,
                            mobile = User.Mobile,
                            telephone = User.Telephone,
                            email = User.Email,
                            description = User.Description,
                            completeAddress = User.CompleteAddress,
                            country = User.Country,
                            longitude = User.Longitude,
                            latitude = User.Latitude,
                            town = User.Town,
                            externalId = User.TenantId,
                            currencySymbol = User.CurrencySymbol,
                        }                                           

                    }
                };

                return model;
            });

            return task;
        }

        #region private function

        private string GetUserPermissions()
        {
            var array = _mobilePermission.GetPermissionByUserId(User.Id);
            string result = string.Join(",", array);
            return result;
        }

        #endregion

    }
}