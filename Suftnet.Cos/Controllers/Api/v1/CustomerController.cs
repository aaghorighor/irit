namespace Suftnet.Cos.Mobile
{
    using Cos.DataAccess;   
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Web.Command;
    using Extension; 
    using System.Web;
    using System.Threading.Tasks;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.Web.ActionFilter;

    [RoutePrefix("api/v1/customer")]
    public class CustomerController : BaseController
    {         
        private readonly IUser _user;
        private readonly ICustomer _customer;
              private readonly ICustomerBoostrapCommand _boostrapCommand;
        private readonly ICreateUserCommand _createUserCommand;
       
        public CustomerController(IUser user, ICustomerBoostrapCommand boostrapCommand,
            ICreateUserCommand createUserCommand, ICustomer customer)
        {
            _user = user;       
                
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
        [JwtAuthenticationAttribute]
        [Route("create")]
        public async Task<IHttpActionResult> Create([FromBody]CreateCustomerDto createCustomerDto)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var user = _user.CheckEmailAddress(createCustomerDto.Email, createCustomerDto.ExternalId);

            if (user)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = Constant.InvalidEmailReasonPhrase }));
            }

            _createUserCommand.User = createCustomerDto;         
            _createUserCommand.VIEW_PATH = HttpContext.Current.Server.MapPath("~/App_Data/Email/newCustomer.html");
            _createUserCommand.Execute();

            if(!_createUserCommand.FLAG)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, new { Message = Constant.USER_ACCOUNT_NOT_CREATED }));
            }

            return await OkResult(_createUserCommand.MobileUser);            
        }

        [HttpPost]
        [JwtAuthenticationAttribute]
        [Route("update")]
        public IHttpActionResult Update([FromBody]UpdateCustomerDto upadteCustomerDto)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var user = _user.CheckEmailAddress(upadteCustomerDto.Email, upadteCustomerDto.ExternalId);

            if (!user)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = Constant.USER_NOT_FOUND }));
            }

            _customer.Update(upadteCustomerDto);

            return Ok(true);
        }

        [HttpPost]
        [JwtAuthenticationAttribute]
        [Route("updateFcmToken")]
        public IHttpActionResult UpdateFcmToken([FromBody]UpdateFcmTokenDto updateFcmTokenDto)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }
           
           _customer.UpdateFcmToken(updateFcmTokenDto);

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