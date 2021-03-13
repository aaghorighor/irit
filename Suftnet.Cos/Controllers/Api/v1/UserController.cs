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
    using Suftnet.Cos.Common;
    using Suftnet.Cos.Services.Interface;

    [RoutePrefix("api/v1/user")]
    public class UserController : BaseController
    {         
        private readonly IUser _user;
        private readonly IJwToken _jwToken;
        private readonly IMobilePermission _mobilePermission;
        private readonly IUserBoostrapCommand _boostrapCommand;

        public UserController(IUser user, IUserBoostrapCommand boostrapCommand,
             IMobilePermission mobilePermission,
             IJwToken jwToken)
        {
            _user = user;                 
            _boostrapCommand = boostrapCommand;
            _jwToken = jwToken;
            _mobilePermission = mobilePermission;
        }        

        [Route("Ping")]
        public IHttpActionResult Get()
        {
            return Ok(DateTime.Now);
        }     
        
        [HttpGet]
        [Route("verifyUser")]
        public IHttpActionResult VerifyCode([FromUri]VerifyUser param)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var user = _user.VerifyUser(new Guid(param.ExternalId), param.UserCode);
            if (user == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = Constant.USER_NOT_FOUND }));
            }

            return OkResult(user);
        }

        [HttpGet]
        [Route("verifyUserByOtp")]
        public IHttpActionResult VerifyUserByOtp([FromUri] AccessCodeModel param)
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

            return OkResult(user);
        }

        #region private function       
        private IHttpActionResult OkResult(MobileTenantDto user)
        {            
            dynamic model = new
            {
                userId = user.Id,
                externalId = user.ExternalId,
                firstName = user.FirstName.EmptyOrNull(),
                lastName = user.LastName.EmptyOrNull(),
                areaId = user.AreaId,
                area = user.Area,             
                phoneNumber = user.PhoneNumber.EmptyOrNull(),
                userName = user.UserName.EmptyOrNull(),
                permissions = GetUserPermissions(user),
                jwtToken = _jwToken.Create(user.UserName, user.Id, user.TenantId.ToString())
            };

            return Ok(model);
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