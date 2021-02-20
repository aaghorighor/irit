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
    using System.Threading.Tasks;
    using Suftnet.Cos.Common;

    [RoutePrefix("api/v1/user")]
    public class UserController : BaseController
    {         
        private readonly IUser _user;     
      
        private readonly IUserBoostrapCommand _boostrapCommand;

        public UserController(IUser user, IUserBoostrapCommand boostrapCommand)
        {
            _user = user;                 
            _boostrapCommand = boostrapCommand;
        }        

        [Route("Ping")]
        public IHttpActionResult Get()
        {
            return Ok(DateTime.Now);
        }     
        
        [HttpGet]
        [Route("verifyUser")]
        public async Task<IHttpActionResult> VerifyCode([FromUri]VerifyUser param)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var user = _user.VerifyUser(new Guid(param.ExternalId), param.UserCode);
            if (user == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, new { Message = Constant.USER_NOT_FOUND }));
            }

            return await OkResult(user);
        }

        [HttpGet]
        [Route("verifyUserByOtp")]
        public async Task<IHttpActionResult> VerifyUserByOtp([FromUri] AccessCodeModel param)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var user = _user.VerifyAccessCode(param.Otp, param.EmailAddress, param.AppCode);
            if (user == null)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound, new { Message = Constant.USER_NOT_FOUND }));
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
        #endregion
    }
}