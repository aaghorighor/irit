namespace Suftnet.Cos.Mobile
{
    using Suftnet.Cos.Extension;
    using Suftnet.Cos.Web.Command;
    using Suftnet.Cos.Web.ViewModel;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    [RoutePrefix("api/v1/delivery")]
    public class DeliveryController : BaseController
    {
        private readonly ICreateDeliveryOrderCommand _createDeliveryOrderCommand;
    
        public DeliveryController(
            ICreateDeliveryOrderCommand createDeliveryOrderCommand)
        {
            _createDeliveryOrderCommand = createDeliveryOrderCommand;        
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
        public IHttpActionResult Create([FromBody]DeliveryOrderAdapter entityToCreate)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            _createDeliveryOrderCommand.entityToCreate = entityToCreate;
            _createDeliveryOrderCommand.Execute();

            return Ok(_createDeliveryOrderCommand.OrderId);           
        }       

    }
}