namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using ViewModel;    
    using System;
    using System.Threading.Tasks;
    using Suftnet.Cos.Services.Interface;
    using Suftnet.Cos.Extension;

    public class BoostrapCommand : IBoostrapCommand
    {      
        private readonly ICategory _category;
        private readonly IMenu _menu;
        private readonly IAddon _addon;
        private readonly IJwToken _jwToken;
        private readonly IMobilePermission _mobilePermission;

        public BoostrapCommand(
            ICategory category, IAddon addon, IMobilePermission mobilePermission,
             IJwToken jwToken,
            IMenu menu)
        {
            _category = category;
            _menu = menu;
            _addon = addon;
            _jwToken = jwToken;          
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
                    Outlet = new
                    {
                        user = new
                        {
                            firstName = User.FirstName.EmptyOrNull(),
                            lastName = User.LastName.EmptyOrNull(),                         
                            areaId = User.AreaId,
                            area = User.Area.EmptyOrNull(),
                            phoneNumber = User.PhoneNumber.EmptyOrNull(),
                            userName = User.FirstName.EmptyOrNull() + " " + User.LastName.EmptyOrNull(),
                            userId = User.Id,
                            externalId = User.TenantId,
                            permissions = GetUserPermissions(),
                            jwtToken = _jwToken.Create(User.UserName, User.Id,TenantId.ToString())
                        },                       
                        tenant = new
                        {
                            name = User.Name.EmptyOrNull(),
                            mobile = User.Mobile.EmptyOrNull(),
                            telephone = User.Telephone.EmptyOrNull(),
                            email = User.Email.EmptyOrNull(),
                            description = User.Description.EmptyOrNull(),
                            completeAddress = User.CompleteAddress.EmptyOrNull(),
                            country = User.Country.EmptyOrNull(),
                            longitude = User.Longitude.ToDecimal(),
                            latitude = User.Latitude.ToDecimal(),
                            town = User.Town.EmptyOrNull(),
                            externalId = User.TenantId,
                            currencySymbol = User.CurrencySymbol,
                            taxRate = User.TaxRate.ToDecimal(),
                            discountRate = User.DiscountRate.ToDecimal(),
                            deliveryCost = User.DeliveryCost.ToDecimal()
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