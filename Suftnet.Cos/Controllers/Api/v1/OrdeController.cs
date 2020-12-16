namespace Suftnet.Cos.Mobile
{
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Extension;
    using Suftnet.Cos.Web.Command;
    using Suftnet.Cos.Web.ViewModel;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;
      
    [RoutePrefix("api/v1/order")]
    public class OrderController : BaseController
    {
        private readonly ICreateOrderCommand _createOrderCommand;
        private readonly IUpdateOrderCommand _updateOrderCommand;

        public OrderController(IUpdateOrderCommand updateOrderCommand,
            ICreateOrderCommand createOrderCommand)
        {
            _createOrderCommand = createOrderCommand;
            _updateOrderCommand = updateOrderCommand;
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
        public async Task<IHttpActionResult> Create([FromBody]CreateOrder param)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            _createOrderCommand.entityToCreate = param;
            await Task.Run(()=> _createOrderCommand.Execute());

            var order = new
            {
                externalId = param.ExternalId,
                orderId = param.OrderId,
                tableId = param.TableId
            };

            return Ok(order);           
        }

        [HttpPost]
        // [JwtAuthenticationAttribute]
        [Route("update")]
        public async Task<IHttpActionResult> Update([FromBody]OrderAdapter param)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            _updateOrderCommand.OrderAdapter = param;      
             await Task.Run(() => _updateOrderCommand.Execute());

            return Ok(true);
        }

    }
}