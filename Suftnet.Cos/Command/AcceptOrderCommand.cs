namespace Suftnet.Cos.Web.Command
{   
    using Suftnet.Cos.DataAccess;    
    using System;
   
    public class AcceptDeliveryOrderCommand : IAcceptDeliveryOrderCommand
    {
        private readonly IOrder _order;     
        private readonly IDeliveryOrder _deliveryOrder;

        public AcceptDeliveryOrderCommand(
             IDeliveryOrder deliveryOrder,
            IOrder order)
        {        
            _order = order;
            _deliveryOrder = deliveryOrder;           
        }
  
        public Guid StatusId { get; set; }  
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string CreatedBy { get; set; }
        public Guid TenantId { get; set; }
        public DeliveryOrderDto deliveryOrderDto { get; set; }

        public void Execute()
        {
            AcceptOrder();    
        }

        #region private function
        private void AcceptOrder()
        {         
            deliveryOrderDto.Id = Guid.NewGuid();
            deliveryOrderDto.CreatedAt = DateTime.UtcNow;
            deliveryOrderDto.CreatedBy = UserName;
            deliveryOrderDto.UserId = UserId;
          
           _deliveryOrder.Insert(deliveryOrderDto);
           _order.UpdateOrderStatus(deliveryOrderDto.OrderId, StatusId, deliveryOrderDto.CreatedAt, deliveryOrderDto.CreatedBy, UserName, TenantId);
        }        

        #endregion
     }
}