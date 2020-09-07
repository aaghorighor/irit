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
    using Suftnet.Cos.Web.Services;

    [RoutePrefix("api/v2/account")]
    public class AccountController : BaseController
    {         
        private readonly IUser _userAccount;
        private readonly IJwToken _jwToken;
        private readonly IFactoryCommand _factoryCommand;
        private readonly IMobilePermission _mobilePermission;
        private readonly IApiUserManger _apiUserManger;        

        public AccountController(IUser userAccount, IMobilePermission mobilePermission, IApiUserManger apiUserManger,
          IJwToken jwToken, IFactoryCommand factoryCommand)
        {                           
            _userAccount = userAccount;
            _jwToken = jwToken;
            _factoryCommand = factoryCommand;
            _mobilePermission = mobilePermission;
            _apiUserManger = apiUserManger;
        }        

        [Route("Ping")]
        public IHttpActionResult Get()
        {
            return Ok(DateTime.Now);
        }        

        [HttpPost]
        [Route("verifyPhoneNumber")]
        public IHttpActionResult VerifyPhoneNumber([FromBody]VerifyModel varifyModel)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.Error()));
            }

            var applicationUser = _userAccount.GetUserByPhone(varifyModel.Phone, varifyModel.ExternalId.ToDecrypt().ToInt());
            if (applicationUser == null)
            {
                return ResponseMessage(new HttpResponseMessage { StatusCode = HttpStatusCode.Unauthorized });
            }

            var command = _factoryCommand.Create<VerifyPhoneNumberCommand>();
            command.ApplicationUser = applicationUser;
            command.Execute();

            return Ok();
        }
        [HttpPost]
        [Route("verifyCode")]
        public IHttpActionResult VerifyCode([FromBody]AccessCodeModel varifyModel)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, ModelState.Error()));
            }

            var user = _userAccount.VerifyAccessCode(varifyModel.Code.ToEncrypt(), varifyModel.Phone, varifyModel.ExternalId.ToDecrypt().ToInt());
            if (user == null)
            {
                return ResponseMessage(new HttpResponseMessage { StatusCode = HttpStatusCode.Unauthorized });
            }

            return OkResult(user);
        }

        #region private function
       
        private IHttpActionResult OkResult(ApplicationUser applicationUser)
        {
            var permissions = _mobilePermission.GetPermissionByUserId(applicationUser.Id);

            var test = new
            {
                externalId = applicationUser.TenantId.ToString().ToEncrypt(),
                areaId = applicationUser.AreaId,
                memberId = applicationUser.MemberId.ToString().ToEncrypt(),
                permissions = permissions.ArrayToString(),
                token = _jwToken.Create(applicationUser.UserName, applicationUser.Id),
                statusCode = HttpStatusCode.OK
            };

            return Ok(test);
        }
        private IHttpActionResult CreateResult(int flag, string message = "")
        {
            return Ok(new
            {
                externalId = "",
                areaId = "",
                memberId = "",
                permissions ="",
                token = "",
                flag = flag,
                message = message,
                statusCode = HttpStatusCode.Created
            });
        }      

        #endregion
    }
}