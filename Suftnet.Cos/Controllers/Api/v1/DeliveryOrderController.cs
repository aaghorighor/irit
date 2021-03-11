namespace Suftnet.Cos.Mobile
{
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Extension;   
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
        public DeliveryOrderController(ICustomerOrder customerOrder, IOrderDetail orderDetail)
        {
            _customerOrder = customerOrder;
            _orderDetail = orderDetail;
        }

        [HttpGet]
        [Route("ping")]
        public IHttpActionResult Ping()
        {
            return Ok(DateTime.Now);
        }

        [HttpGet]
        // [JwtAuthenticationAttribute]
        [Route("fetchBy")]
        public async Task<IHttpActionResult> FetchBy([FromUri]ExternalParam externalParam)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var model = await Task.Run(() => _customerOrder.FetchBy(new Guid(externalParam.ExternalId), new Guid(eOrderStatus.Completed.ToUpper())));

            return Ok(model);
        }        

        [HttpGet]
        // [JwtAuthenticationAttribute]
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

    }
}