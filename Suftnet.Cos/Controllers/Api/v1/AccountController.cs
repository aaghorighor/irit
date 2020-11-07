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
  
    [RoutePrefix("api/v1/account")]
    public class AccountController : BaseController
    {         
        private readonly IUser _userAccount;
        private readonly IJwToken _jwToken;
        private readonly IFactoryCommand _factoryCommand;
        private readonly IMobilePermission _mobilePermission;           

        public AccountController(IUser userAccount, IMobilePermission mobilePermission,
          IJwToken jwToken, IFactoryCommand factoryCommand)
        {                           
            _userAccount = userAccount;
            _jwToken = jwToken;
            _factoryCommand = factoryCommand;
            _mobilePermission = mobilePermission;      
        }        

        [Route("Ping")]
        public IHttpActionResult Get()
        {
            return Ok(DateTime.Now);
        }     

        [HttpPost]
        [Route("verifyEmailAddress")]
        public IHttpActionResult VerifyEmailAddress([FromBody]VerifyEmailAddressDto param)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.Error()));
            }

            var user = _userAccount.GetUserByUserName(param.EmailAddress, param.AppCode);
            if (user == null)
            {
                return ResponseMessage(new HttpResponseMessage { StatusCode = HttpStatusCode.Unauthorized });
            }

            var command = _factoryCommand.Create<VerifyEmailCommand>();
            command.User = user;
            command.AppCode = param.AppCode;
            command.Execute();

            return Ok();
        }

        [HttpPost]
        [Route("verifyCode")]
        public IHttpActionResult VerifyCode([FromBody]AccessCodeModel param)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.Error()));
            }

            var user = _userAccount.VerifyAccessCode(param.Otp, param.EmailAddress, param.AppCode);
            if (user == null)
            {
                return ResponseMessage(new HttpResponseMessage { StatusCode = HttpStatusCode.Unauthorized });
            }

            return OkResult(user);
        }

        #region private function
       
        private IHttpActionResult OkResult(ApplicationUser user)
        {
            var permissions = _mobilePermission.GetPermissionByUserId(user.Id);

            var test = new
            {
                externalId = user.TenantId.ToString().ToEncrypt(),
                areaId = user.AreaId,              
                permissions = permissions.ArrayToString(),
                token = _jwToken.Create(user.UserName, user.Id),
                statusCode = HttpStatusCode.OK
            };

            return Ok(test);
        }
      
        #endregion
    }
}