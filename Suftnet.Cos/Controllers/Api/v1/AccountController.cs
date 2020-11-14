namespace Suftnet.Cos.Mobile
{
    using Cos.DataAccess;
    using Services.Interface;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Web;
    using Web.Command;
    using Extension; 
    using DataAccess.Identity;
    using System.Web;

    [RoutePrefix("api/v1/account")]
    public class AccountController : BaseController
    {         
        private readonly IUser _user;
        private readonly IJwToken _jwToken;
        private readonly IFactoryCommand _factoryCommand;
        private readonly IMobilePermission _mobilePermission;           

        public AccountController(IUser user, IMobilePermission mobilePermission,
          IJwToken jwToken, IFactoryCommand factoryCommand)
        {
            _user = user;
            _jwToken = jwToken;
            _factoryCommand = factoryCommand;
            _mobilePermission = mobilePermission;      
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

            var user = _user.GetUserByUserName(param.EmailAddress, param.AppCode.ToInt());
            if (user == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "NotFound" }));
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
        public IHttpActionResult VerifyCode([FromUri] AccessCodeModel param)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var user = _user.VerifyAccessCode(param.Otp, param.EmailAddress, param.AppCode);
            if (user == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "NotFound" }));
            }

            return OkResult(user);
        }

        #region private function
       
        private IHttpActionResult OkResult(MobileTenantDto user)
        {
            var permissions = _mobilePermission.GetPermissionByUserId(user.Id);

            var model = new
            {
                user = new {
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    area = user.LastName,
                    areaId = user.AreaId,
                    phoneNumber = user.PhoneNumber,
                    userName = user.UserName,
                    externalId = user.Id
                },
                tenant = new {
                    name = user.Name,
                    mobile = user.Mobile,
                    telephone = user.Telephone,
                    email = user.Email,
                    description = user.Description,                 
                    completeAddress = user.CompleteAddress,
                    country = user.Country,
                    longitude = user.Longitude,
                    latitude = user.Latitude,
                    town = user.Town,
                    externalId = user.TenantId
                },
                permissions = permissions.ArrayToString(),
                token = _jwToken.Create(user.UserName, user.Id),
                statusCode = HttpStatusCode.OK
            };

            return Ok(model);
        }
      
        #endregion
    }
}