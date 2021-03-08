namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;    
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CloseOrderCommand : ICloseOrderCommand
    {
        private readonly IOrder _order;
        private readonly IOrderDetail _orderDetail;
        private readonly IFactoryCommand _factoryCommand;
        private readonly ICustomerOrder _customerOrder;

        public CloseOrderCommand(IFactoryCommand factoryCommand, ICustomerOrder customerOrder,
            IOrder order, IOrderDetail orderDetail)
        {
            _factoryCommand = factoryCommand;
            _order = order;
            _customerOrder = customerOrder;
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
            System.Threading.Tasks.Task.Run(() => OnPushNotification());
        }

        #region private function
        private void CompleteOrder()
        {
            _order.UpdateOrderStatus(OrderId, StatusId, CreatedDt, CreatedBy, TenantId);
            _orderDetail.UpdateCompletedOrders(OrderId, CreatedBy);

             Baskets = _orderDetail.FetchBasket(OrderId);

        }

        private void OnPushNotification()
        {
            var fcmToken = GetCustomerFcmToken();

            if(!string.IsNullOrEmpty(fcmToken))
            {
                var command = _factoryCommand.Create<PushNotificationCommand>();
                command.MessageTypeId = MessageType.OrderStatus;
                command.OrderStatusId = eOrderStatus.Ready;
                command.FcmToken = GetCustomerFcmToken();
                command.Execute();
            }
            
        }

        private string GetCustomerFcmToken()
        {
           return _customerOrder.FetchByFcmToken(OrderId);
        }

        #endregion

        }
}