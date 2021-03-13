namespace Suftnet.Cos.Mobile
{
    using Cos.DataAccess;   
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Web;
    using Web.Command;
    using Extension; 
    using System.Web;
    using System.Threading.Tasks;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.Services.Interface;
    using Suftnet.Cos.Web.ViewModel;

    [RoutePrefix("api/v1/account")]
    public class AccountController : BaseController
    {         
        private readonly IUser _user;      
        private readonly IFactoryCommand _factoryCommand;    
        private readonly IBoostrapCommand _boostrapCommand;
        private readonly IJwToken _jwToken;
        private readonly IMobilePermission _mobilePermission;

        public AccountController(IUser user, IBoostrapCommand boostrapCommand,
         IJwToken jwToken, IMobilePermission mobilePermission,
         IFactoryCommand factoryCommand)
        {
            _mobilePermission = mobilePermission;
            _jwToken = jwToken;
            _user = user;         
            _factoryCommand = factoryCommand;         
            _boostrapCommand = boostrapCommand;
        }        

        [Route("Ping")]
        public IHttpActionResult Get()
        {
            return Ok(DateTime.Now);
        }     

        [HttpGet]
        [Route("verifyAppCode")]
        public IHttpActionResult VerifyAppCode([FromUri] VerifyAppCode param)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var user = _user.GetUserByUserName(param.EmailAddress, param.AppCode);
            if (user == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = Constant.USER_NOT_FOUND }));
            }

            var command = _factoryCommand.Create<VerifyEmailCommand>();
            command.User = user;
            command.AppCode = param.AppCode.ToInt();
            command.VIEW_PATH = HttpContext.Current.Server.MapPath("~/App_Data/Email/otp.html");
            command.Execute();

            return Ok();
        }
        [HttpGet]
        [Route("verifyAccessCode")]
        public async Task<IHttpActionResult> VerifyCode([FromUri] AccessCodeModel param)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var user = _user.VerifyAccessCode(param.Otp, param.EmailAddress, param.AppCode);
            if (user == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = Constant.USER_NOT_FOUND }));
            }

            if(user.AreaId ==
                (int)eArea.DeliveryOffice)
            {                
                return await OkResult(user, true);
            }

            return await OkResult(user);
        }

        #region private function
       
        private async Task<IHttpActionResult> OkResult(MobileTenantDto user)
        {
            _boostrapCommand.User = user;
            _boostrapCommand.TenantId = user.TenantId;
             var model = await Task.Run(() => _boostrapCommand.Execute());

            return Ok(model);
        }
        private async Task<IHttpActionResult> OkResult(MobileTenantDto user, bool delivery = true)
        {
            dynamic Outlet = new
            {
                user = new
                {                   
                    firstName = user.FirstName.EmptyOrNull(),
                    lastName = user.LastName.EmptyOrNull(),
                    areaId = user.AreaId,
                    area = user.Area.EmptyOrNull(),
                    phoneNumber = user.PhoneNumber.EmptyOrNull(),
                    userName = user.FirstName.EmptyOrNull() + " " + user.LastName.EmptyOrNull(),
                    userId = user.Id,
                    externalId = user.TenantId,
                    permissions = GetUserPermissions(user),
                    jwtToken = _jwToken.Create(user.UserName, user.Id, user.TenantId.ToString())
                },
                tenant = new
                {
                    name = user.Name.EmptyOrNull(),
                    mobile = user.Mobile.EmptyOrNull(),
                    telephone = user.Telephone.EmptyOrNull(),
                    email = user.Email.EmptyOrNull(),
                    description = user.Description.EmptyOrNull(),
                    completeAddress = user.CompleteAddress.EmptyOrNull(),
                    country = user.Country.EmptyOrNull(),
                    longitude = user.Longitude.ToDecimal(),
                    latitude = user.Latitude.ToDecimal(),
                    town = user.Town.EmptyOrNull(),
                    externalId = user.TenantId,
                    currencySymbol = user.CurrencySymbol,
                    taxRate = user.TaxRate.ToDecimal(),
                    discountRate = user.DiscountRate.ToDecimal(),
                    deliveryCost = user.DeliveryCost.ToDecimal()
                }
            };

            var boostrapModel = new BoostrapModel
            {
                 Outlet = Outlet
            };

            var result = await Task.Run(() => boostrapModel);
            return Ok(result);
        }
        private string GetUserPermissions(MobileTenantDto user)
        {
            var array = _mobilePermission.GetPermissionByUserId(user.Id);
            string result = string.Join(",", array);
            return result;
        }

        #endregion
    }
}