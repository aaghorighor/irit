namespace Suftnet.Cos.Mobile
{
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Extension;
    using Suftnet.Cos.Web.Command;
    using Suftnet.Cos.Web.ViewModel;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web.Http;

    [RoutePrefix("api/v1/customerDeliveryOrder")]
    public class CustomerDeliveryOrderController : BaseController
    {
        private readonly ICreateDeliveryOrderCommand _createDeliveryOrderCommand;
        private readonly ICustomerOrder _customerOrder;
        private readonly IOrderDetail _orderDetail;
        private readonly IOrderPaymentCommand _orderPaymentCommand;

        public CustomerDeliveryOrderController(ICustomerOrder customerOrder, IOrderDetail orderDetail, IOrderPaymentCommand orderPaymentCommand,
        ICreateDeliveryOrderCommand createDeliveryOrderCommand)
        {
            _customerOrder = customerOrder;
            _orderDetail = orderDetail;
            _createDeliveryOrderCommand = createDeliveryOrderCommand;
            _orderPaymentCommand = orderPaymentCommand;
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
        public async Task<IHttpActionResult> Fetch([FromUri]CustomerOrderQuery customerOrderQuery)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var model = await Task.Run(() => _customerOrder.Fetch(new Guid(customerOrderQuery.CustomerId)));

            return Ok(model);
        }        

        [HttpGet]
        // [JwtAuthenticationAttribute]
        [Route("fetchBasket")]
        public async Task<IHttpActionResult> FetchBasket([FromUri] OrderQuery orderQuery)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var model = await Task.Run(() => _orderDetail.FetchMobileBasket(new Guid(orderQuery.OrderId)));

            return Ok(model);
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

            _orderPaymentCommand.entityToCreate = entityToCreate;
            _orderPaymentCommand.Execute();

            if(_orderPaymentCommand.Error)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = _orderPaymentCommand.Reason }));
            }

            _createDeliveryOrderCommand.entityToCreate = entityToCreate;
            _createDeliveryOrderCommand.Execute();

            return Ok(entityToCreate.OrderId);           
        }       

    }
}