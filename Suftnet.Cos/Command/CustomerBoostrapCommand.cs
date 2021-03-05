namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using ViewModel;    
    using System;
    using System.Threading.Tasks;
    using Suftnet.Cos.Services.Interface;
    using Suftnet.Cos.Extension;

    public class CustomerBoostrapCommand : ICustomerBoostrapCommand
    {      
        private readonly ICategory _category;
        private readonly IMenu _menu;
        private readonly IAddon _addon;
        private readonly IJwToken _jwToken;
        private readonly IMobilePermission _mobilePermission;
        private readonly ITax _tax;
        private readonly IDiscount _discount;

        public CustomerBoostrapCommand(
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
                            customerId = User.CustomerId,
                            permissions = GetUserPermissions(),
                            fcmToken = "",
                            jwtToken = _jwToken.Create(User.UserName, User.Id)
                        },                       
                        tenant = new
                        {
                            name = User.Tenant.Name.EmptyOrNull(),
                            mobile = User.Tenant.Mobile.EmptyOrNull(),
                            telephone = User.Tenant.Telephone.EmptyOrNull(),
                            email = User.Tenant.Email.EmptyOrNull(),
                            description = User.Tenant.Description.EmptyOrNull(),
                            completeAddress = User.Tenant.CompleteAddress.EmptyOrNull(),
                            country = User.Tenant.Country.EmptyOrNull(),
                            longitude = User.Tenant.Longitude.ToDecimal(),
                            latitude = User.Tenant.Latitude.ToDecimal(),
                            town = User.Tenant.Town.EmptyOrNull(),
                            externalId = User.TenantId,
                            taxRate = User.Tenant.TaxRate.ToDecimal(),
                            discountRate = User.Tenant.DiscountRate.ToDecimal(),
                            deliveryCost = User.Tenant.DeliveryCost.ToDecimal(),
                            currencySymbol = User.Tenant.CurrencySymbol,
                            stripePublishableKey = User.Tenant.StripePublishableKey.EmptyOrNull()
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