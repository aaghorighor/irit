namespace Suftnet.Cos.Controllers
{
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Common;
    using System.Web.Mvc;
    using System;
       
    public class KitchenController : KitchenBaseController
    {
        private readonly IOrder _order;
        private readonly lOrderDetail _orderDetail;

        public KitchenController(IOrder order,
            lOrderDetail orderDetail)
        {
            _order = order;
            _orderDetail = orderDetail;
        }

        public ActionResult Index()
        {
            return View(_orderDetail.GetTableOrder((int)OrderStatus.Occupied, (int)OrderStatus.Complete));
        }

        [HttpPost]
        public JsonResult Update(int Id)
        {
            var order = _order.Get(Id);

            if(order != null)
            {
                if (order.OrderTypeId != (int)OrderType.Delivery)
                {
                    if (order.StatusId == (int)OrderStatus.Processing)
                    {
                        _order.UpdateOrderStatus(Id, (int)OrderStatus.Occupied, DateTime.UtcNow, this.UserName);
                    }
                    else if (order.StatusId == (int)OrderStatus.Complete)
                    {
                        _order.UpdateOrderStatus(Id, (int)OrderStatus.Complete, DateTime.UtcNow, this.UserName);
                    }
                }
                else
                {
                    _order.UpdateOrderStatus(Id, (int)OrderStatus.Occupied, DateTime.UtcNow, this.UserName);
                }               
            }

            _orderDetail.UpdateCompletedOrders(Id);

            return Json(new { orderId = Id, ok = true }, JsonRequestBehavior.AllowGet);
        }
    }
}