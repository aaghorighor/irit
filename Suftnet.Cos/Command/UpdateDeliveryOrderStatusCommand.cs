namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;    
    using System;
   
    public class UpdateDeliveryOrderStatusCommand : IUpdateDeliveryOrderStatusCommand
    {
        private readonly IOrder _order;      
        private readonly IFactoryCommand _factoryCommand;
        private readonly ICustomerOrder _customerOrder;
       
        public UpdateDeliveryOrderStatusCommand(IFactoryCommand factoryCommand,
            ICustomerOrder customerOrder,
            IOrder order)
        {
            _factoryCommand = factoryCommand;
            _order = order;          
            _customerOrder = customerOrder;      
        }
  
        public Guid OrderId { get; set; }
        public Guid StatusId { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid TenantId { get; set; }
       
        public void Execute()
        {           
            System.Threading.Tasks.Task.Run(() => OnPushNotification());
            UpdateOrderStatus();
        }

        #region private function
        private void UpdateOrderStatus()
        {                                       
           _order.UpdateOrderStatus(OrderId, StatusId, CreatedAt, CreatedBy, UserName, TenantId);
        }

        private void OnPushNotification()
        {
            var fcmToken = GetCustomerFcmToken();

            if(!string.IsNullOrEmpty(fcmToken))
            {
                var command = _factoryCommand.Create<PushNotificationCommand>();
                command.MessageTypeId = MessageType.OrderStatus;

                if(StatusId == new Guid(eOrderStatus.Dispatched))
                {
                    command.OrderStatusId = eOrderStatus.Dispatched;
                }else if (StatusId == new Guid(eOrderStatus.Delivered))
                {
                    command.OrderStatusId = eOrderStatus.Delivered;
                }              

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