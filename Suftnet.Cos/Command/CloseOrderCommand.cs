namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;    
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CloseOrderCommand : ICloseOrderCommand
    {
        private readonly IOrder _order;
        private readonly IOrderDetail _orderDetail;

        public CloseOrderCommand(
            IOrder order, IOrderDetail orderDetail)
        {
            _order = order;
            _orderDetail = orderDetail;

        }

        public Guid OrderId { get; set; }
        public Guid StatusId { get; set; }
        public DateTime CreatedDt { get; set; }
        public string CreatedBy { get; set; }
        public Guid TenantId { get; set; }
        public IList<BasketDto> Baskets { get; set; }

        public void Execute()
        {
            CompleteOrder();
        }

        #region private function
        private void CompleteOrder()
        {
            _order.UpdateOrderStatus(OrderId, StatusId, CreatedDt, CreatedBy, OrderId);
            _orderDetail.UpdateCompletedOrders(OrderId, CreatedBy);
             Baskets = _orderDetail.FetchOrder(OrderId);
        }
        #endregion

    }
}