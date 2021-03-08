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
      
    [RoutePrefix("api/v1/orderCart")]
    public class OrderCartController : BaseController
    {
        private readonly IOrderDetail _cart;
 
        public OrderCartController(IOrderDetail cart)
        {
            _cart = cart;           
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
        public async Task<IHttpActionResult> Fetch([FromUri]ExternalParam param)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var model = await Task.Run(() => _cart.FetchKitchenOrders(new Guid(eOrderStatus.Processing), new Guid(param.ExternalId.ToUpper())));

            return Ok(model);
        }

        [HttpGet]
        // [JwtAuthenticationAttribute]
        [Route("fetchKitchenDeliveryOrders")]
        public async Task<IHttpActionResult> FetchKitchenDeliveryOrders([FromUri]ExternalParam param)
        {
            if (!ModelState.IsValid)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = ModelState.Error() }));
            }

            var model = await Task.Run(() => _cart.FetchKitchenDeliveryOrders(new Guid(eOrderStatus.Processing), new Guid(param.ExternalId.ToUpper()), new Guid(eOrderType.Delivery)));

            return Ok(model);
        }

    }
}