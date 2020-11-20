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

    [RoutePrefix("api/v1/account")]
    public class AccountController : BaseController
    {         
        private readonly IUser _user;      
        private readonly IFactoryCommand _factoryCommand;    
        private readonly IBoostrapCommand _boostrapCommand;

        public AccountController(IUser user, IBoostrapCommand boostrapCommand,
         IFactoryCommand factoryCommand)
        {
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
        public async Task<IHttpActionResult> VerifyCode([FromUri] AccessCodeModel param)
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