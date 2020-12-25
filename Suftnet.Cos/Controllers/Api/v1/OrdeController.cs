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

        public OrderController(IUpdateOrderCommand updateOrderCommand,
            IOrder order,
            ICloseOrderCommand closeOrderCommand,
            ICreateOrderCommand createOrderCommand)
        {
            _createOrderCommand = createOrderCommand;
            _updateOrderCommand = updateOrderCommand;
            _order = order;
            _closeOrderCommand = closeOrderCommand;
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
        public async Task<IHttpActionResult> Fetch([FromUri] OrderParam param)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var model = await Task.Run(() => _order.FetchOrder(new Guid(param.OrderId)));

            return Ok(model);
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

        [HttpPost]
        // [JwtAuthenticationAttribute]
        [Route("done")]
        public async Task<IHttpActionResult> Done([FromBody]OrderDone param)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }
                      
            _closeOrderCommand.OrderId = new Guid(param.orderId);       
            _closeOrderCommand.CreatedBy = param.userName;
            _closeOrderCommand.TenantId = new Guid(param.externalId);
            _closeOrderCommand.CreatedDt = param.updateDate;
            _closeOrderCommand.StatusId = new Guid(eOrderStatus.Completed.ToUpper());

            await Task.Run(() => _closeOrderCommand.Execute());     
           
            return Ok(_closeOrderCommand.Baskets);
        }

    }
}