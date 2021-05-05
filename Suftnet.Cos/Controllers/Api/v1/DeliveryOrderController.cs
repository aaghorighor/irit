namespace Suftnet.Cos.Mobile
{
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Extension;
    using Suftnet.Cos.Web.ActionFilter;
    using Suftnet.Cos.Web.Command;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    [RoutePrefix("api/v1/deliveryOrder")]
    public class DeliveryOrderController : BaseController
    {     
        private readonly ICustomerOrder _customerOrder;
        private readonly IOrderDetail _orderDetail;
        private readonly IDeliveryOrder _deliveryOrder;
        private readonly IAcceptDeliveryOrderCommand _acceptDeliveryOrderCommand;
        private readonly IUpdateDeliveryOrderStatusCommand _updateDeliveryOrderStatusCommand;
        public DeliveryOrderController(ICustomerOrder customerOrder, IUpdateDeliveryOrderStatusCommand updateDeliveryOrderStatusCommand,
            IDeliveryOrder deliveryOrder, IAcceptDeliveryOrderCommand acceptDeliveryOrderCommand,
            IOrderDetail orderDetail)
        {
            _customerOrder = customerOrder;
            _orderDetail = orderDetail;
            _deliveryOrder = deliveryOrder;
            _acceptDeliveryOrderCommand = acceptDeliveryOrderCommand;
            _updateDeliveryOrderStatusCommand = updateDeliveryOrderStatusCommand;
        }

        [HttpGet]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok(DateTime.Now);
        }

        [HttpGet]
        [JwtAuthenticationAttribute]
        [Route("fetchByPaging")]
        public async Task<IHttpActionResult> FetchByPaging([FromUri] DeliveryOrderPager deliveryOrderPager)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var model = await Task.Run(() => _customerOrder.FetchBy(
               new Guid(ExternalId), deliveryOrderPager.StatusId, (deliveryOrderPager.Page * deliveryOrderPager.Count), deliveryOrderPager.Count, deliveryOrderPager.Query));

            return Ok(new
            {
                total = _customerOrder.Count(new Guid(ExternalId), new Guid(deliveryOrderPager.StatusId)),
                count = model.Count,
                deliveryOrders = model
            });
        }

        [HttpGet]
        [JwtAuthenticationAttribute]
        [Route("fetchByJob")]
        public async Task<IHttpActionResult> FetchByJob([FromUri] ExternalParam externalParam)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var model = await Task.Run(() => _customerOrder.FetchByJob(new Guid(externalParam.ExternalId), new Guid(eOrderStatus.Ready.ToUpper())));

            return Ok(model);
        }

        [HttpGet]
        [JwtAuthenticationAttribute]
        [Route("fetchByOrder")]
        public async Task<IHttpActionResult> FetchByOrder([FromUri]OrderQuery orderQuery)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var model = await Task.Run(() => _orderDetail.FetchMobileBasket(new Guid(orderQuery.OrderId)));

            return Ok(model);
        }

        [HttpGet]
        [JwtAuthenticationAttribute]
        [Route("fetchByDriver")]
        public async Task<IHttpActionResult> FetchByDriver([FromUri]DriveQuery driveQuery)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var model = await Task.Run(() => _deliveryOrder.FetchBy(driveQuery.UserId, new Guid(eOrderStatus.Delivered.ToUpper())));

            return Ok(model);
        }

        [HttpGet]
        [JwtAuthenticationAttribute]
        [Route("fetchByDelivered")]
        public async Task<IHttpActionResult> FetchByDelivered([FromUri] DriveQuery driveQuery)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var model = await Task.Run(() => _deliveryOrder.FetchByDelivered(driveQuery.UserId, new Guid(eOrderStatus.Delivered.ToUpper())));

            return Ok(model);
        }

        [HttpPost]
        [JwtAuthenticationAttribute]
        [Route("create")]
        public async Task<IHttpActionResult> Create([FromBody]DeliveryOrderDto deliveryOrderDto)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            _acceptDeliveryOrderCommand.deliveryOrderDto = deliveryOrderDto;    
            _acceptDeliveryOrderCommand.UserId = this.UserId;          
            _acceptDeliveryOrderCommand.UserName = this.UserName;         
            _acceptDeliveryOrderCommand.TenantId = new Guid(this.ExternalId);
            _acceptDeliveryOrderCommand.StatusId = new Guid(eOrderStatus.Accepted);

            await Task.Run(()=> _acceptDeliveryOrderCommand.Execute());

            return Ok(true);
        }

        [HttpPost]
        [JwtAuthenticationAttribute]
        [Route("updateStatus")]
        public async Task<IHttpActionResult> UpdateStatus([FromBody]UpdateDeliveryStatusDto updateDeliveryStatusDto)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }
         
            _updateDeliveryOrderStatusCommand.UserId = this.UserId;
            _updateDeliveryOrderStatusCommand.CreatedAt = DateTime.UtcNow;
            _updateDeliveryOrderStatusCommand.UserName = this.UserName;
            _updateDeliveryOrderStatusCommand.TenantId = new Guid(this.ExternalId);
            _updateDeliveryOrderStatusCommand.OrderId = updateDeliveryStatusDto.OrderId;
            _updateDeliveryOrderStatusCommand.StatusId = updateDeliveryStatusDto.StatusId;

            await Task.Run(() => _updateDeliveryOrderStatusCommand.Execute());

            return Ok(true);
        }
    }
}