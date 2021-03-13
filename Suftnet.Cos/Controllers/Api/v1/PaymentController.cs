namespace Suftnet.Cos.Mobile
{
    using Suftnet.Cos.Common;
    using Suftnet.Cos.Extension;
    using Suftnet.Cos.Web;
    using Suftnet.Cos.Web.ActionFilter;
    using Suftnet.Cos.Web.Command;  
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
      
    [RoutePrefix("api/v1/payment")]
    public class PaymentController : BaseController
    {
        private readonly IPaymentCommand _paymentCommand;
 
        public PaymentController(IPaymentCommand paymentCommand)
        {
            _paymentCommand = paymentCommand;           
        }

        [HttpGet]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok(DateTime.Now);
        }
   
        [HttpPost]
        [JwtAuthenticationAttribute]
        [Route("create")]
        public async Task<IHttpActionResult> Create([FromBody]PaymentModel paymentModel)
        {
            if (!ModelState.IsValid)
            {return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() })); }

            paymentModel.AccountTypeId = eAccountType.DineIn;
           _paymentCommand.paymentModel = paymentModel;      
            await Task.Run(() => _paymentCommand.Execute());

            return Ok(true);
        }

    }
}