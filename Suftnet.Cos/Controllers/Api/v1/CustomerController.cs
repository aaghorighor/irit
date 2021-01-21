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
    using Suftnet.Cos.Web.Services.Implementation;
    using Suftnet.Cos.DataAccess.Identity;

    [RoutePrefix("api/v1/customer")]
    public class CustomerController : BaseController
    {         
        private readonly IUser _user;
        private readonly ICustomer _customer;
        private readonly IFactoryCommand _factoryCommand;    
        private readonly IBoostrapCommand _boostrapCommand;
        private readonly ICreateUserCommand _createUserCommand;
       
        public CustomerController(IUser user,IBoostrapCommand boostrapCommand,
            ICreateUserCommand createUserCommand, ICustomer customer,
         IFactoryCommand factoryCommand)
        {
            _user = user;         
            _factoryCommand = factoryCommand;         
            _boostrapCommand = boostrapCommand;
            _createUserCommand = createUserCommand;
            _customer = customer;
        }        

        [Route("Ping")]

        public IHttpActionResult Get()
        {
            return Ok(DateTime.Now);
        }

        [HttpPost]
        // [JwtAuthenticationAttribute]
        [Route("create")]
        public async Task<IHttpActionResult> Create([FromBody]CreateCustomerDto param)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var user = _user.CheckEmailAddress(param.Email, param.ExternalId);

            if (user)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = Constant.InvalidEmailReasonPhrase }));
            }

            _createUserCommand.User = param;         
            _createUserCommand.VIEW_PATH = HttpContext.Current.Server.MapPath("~/App_Data/Email/otp.html");
            _createUserCommand.Execute();

            if(!_createUserCommand.FLAG)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = Constant.USER_ACCOUNT_NOT_CREATED }));
            }

            return await OkResult(_createUserCommand.MobileUser);            
        }

        [HttpPost]
        // [JwtAuthenticationAttribute]
        [Route("update")]
        public IHttpActionResult Update([FromBody]UpadteCustomerDto param)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var user = _user.CheckEmailAddress(param.Email, new Guid(param.ExternalId));

            if (!user)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = Constant.USER_NOT_FOUND }));
            }

            _customer.Update(param.CustomerDto);

            return Ok(true);
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