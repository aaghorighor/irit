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
        private readonly ICloseOrderCommand _closeOrderCommand;
        private readonly IOrder _order;
        private readonly ICancelOrderCommand _cancelOrderCommand;

        public OrderController(IUpdateOrderCommand updateOrderCommand,
            IOrder order, ICancelOrderCommand cancelOrderCommand,
            ICloseOrderCommand closeOrderCommand,
            ICreateOrderCommand createOrderCommand)
        {
            _createOrderCommand = createOrderCommand;
            _updateOrderCommand = updateOrderCommand;
            _order = order;
            _closeOrderCommand = closeOrderCommand;
            _cancelOrderCommand = cancelOrderCommand;
        }

        [HttpGet]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok(DateTime.Now);
        }

        [HttpGet]
        // [JwtAuthenticationAttribute]
        [Route("fetch")]
        public async Task<IHttpActionResult> Fetch([FromUri] OrderQuery orderQuery)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var model = await Task.Run(() => _order.FetchOrder(new Guid(orderQuery.OrderId)));

            return Ok(model);
        }

        [HttpPost]
       // [JwtAuthenticationAttribute]
        [Route("create")]
        public async Task<IHttpActionResult> Create([FromBody]CreateOrder createOrder)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            _createOrderCommand.entityToCreate = createOrder;
            await Task.Run(()=> _createOrderCommand.Execute());

            var order = new {externalId = createOrder.ExternalId,orderId = createOrder.OrderId,
                tableId = createOrder.TableId
            };

            return Ok(order);           
        }

        [HttpPost]
        // [JwtAuthenticationAttribute]
        [Route("update")]
        public async Task<IHttpActionResult> Update([FromBody]OrderAdapter orderAdapter)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            _updateOrderCommand.OrderAdapter = orderAdapter;      
             await Task.Run(() => _updateOrderCommand.Execute());

            return Ok(true);
        }

        [HttpPost]
        // [JwtAuthenticationAttribute]
        [Route("done")]
        public async Task<IHttpActionResult> Done([FromBody]OrderDone orderDone)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }
                      
            _closeOrderCommand.OrderId = new Guid(orderDone.orderId);       
            _closeOrderCommand.CreatedBy = orderDone.userName;
            _closeOrderCommand.TenantId = new Guid(orderDone.externalId);
            _closeOrderCommand.CreatedDt = orderDone.updateDate;
            _closeOrderCommand.StatusId = new Guid(eOrderStatus.Completed.ToUpper());

            await Task.Run(() => _closeOrderCommand.Execute());     
           
            return Ok(_closeOrderCommand.Baskets);
        }

        [HttpPost]
        // [JwtAuthenticationAttribute]
        [Route("Cancel")]
        public async Task<IHttpActionResult> Cancel([FromBody]CancelOrder cancelOrder)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            _cancelOrderCommand.OrderId = new Guid(cancelOrder.orderId);
            _cancelOrderCommand.UserName = cancelOrder.userName;
            _cancelOrderCommand.TenantId = new Guid(cancelOrder.externalId);
            _cancelOrderCommand.UpdateDate = cancelOrder.updateDate;
            _cancelOrderCommand.TableId = new Guid(cancelOrder.tableId);

            await Task.Run(() => _cancelOrderCommand.Execute());

            return Ok(true);
        }

    }
}