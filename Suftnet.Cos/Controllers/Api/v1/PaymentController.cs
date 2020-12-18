namespace Suftnet.Cos.Mobile
{ 
    using Suftnet.Cos.Extension;
    using Suftnet.Cos.Web;
    using Suftnet.Cos.Web.Command;  
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
      
    [RoutePrefix("api/v1/payment")]
    public class PaymentController : BaseController
    {
        private readonly IPaymentCommand _command;
 
        public PaymentController(IPaymentCommand paymentCommand)
        {
            _command = paymentCommand;           
        }

        [HttpGet]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok(DateTime.Now);
        }
   
        [HttpPost]
        // [JwtAuthenticationAttribute]
        [Route("create")]
        public async Task<IHttpActionResult> Create([FromBody]PaymentModel model)
        {
            if (!ModelState.IsValid)
            {return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() })); }

            _command.Model = model;      
             await Task.Run(() => _command.Execute());

           return Ok(true);
        }

    }
}