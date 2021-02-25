namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;    
    using System;
    using Suftnet.Cos.Extension;
    using Suftnet.Cos.Common;
     using Suftnet.Cos.Web.ViewModel;
    using System.Collections.Generic;

    public class CreateDeliveryOrderCommand : ICreateDeliveryOrderCommand
    {
        private readonly IOrder _order;
        private readonly ICustomerOrder _customerOrder;
        private readonly ICustomerDeliveryStatus _customerDeliveryStatus;
        private readonly ICustomerOrderDelivery _customerOrderDelivery;
        private readonly IOrderCommand _orderCommand;

        public CreateDeliveryOrderCommand(ICustomerOrderDelivery customerOrderDelivery, IOrderCommand orderCommand,
            IOrder order, ICustomerOrder customerOrder, ICustomerDeliveryStatus customerDeliveryStatus)
        {
            _orderCommand = orderCommand;
            _order = order;
            _customerOrder = customerOrder;
            _customerDeliveryStatus = customerDeliveryStatus;
            _customerOrderDelivery = customerOrderDelivery;
        }               
              
        public DeliveryOrderAdapter entityToCreate { get; set; }
        public Guid OrderId { get; set; }

        public void Execute()
        {
            OrderId = CreateOrder();
            System.Threading.Tasks.Task.Run(() => ExecuteOrder(OrderId));
        }

        #region private function
       
        private Guid CreateOrder()
        {
            var order = new OrderDto
            {
                CreatedDT = DateTime.UtcNow,
                CreatedBy = entityToCreate.UserName,

                UpdateDate = DateTime.UtcNow,
                UpdateBy = entityToCreate.UserName,

                StartDt = DateTime.UtcNow,

                OrderTypeId = new Guid(eOrderType.Delivery),
                StatusId = new Guid(eOrderStatus.Pending.ToUpper()),
                PaymentStatusId = new Guid(ePaymentStatus.Pending),

                TenantId = new Guid(entityToCreate.ExternalId),
                Id = Guid.NewGuid()
            };
           _order.Insert(order);
            return order.Id;
        }
        private Guid CreateCustomerOrder(Guid orderId)
        {
            var customerOrder = new CustomerOrderDto
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                CustomerId = new Guid(entityToCreate.Order.customerId),
                CreatedDT = DateTime.UtcNow,
                CreatedBy = entityToCreate.UserName
            };

            _customerOrder.Insert(customerOrder);
            return customerOrder.Id;
        }
        private Guid CreateCustomerOrderDelivery(Guid customerOrderId)
        {
            var customerOrderDelivery = new CustomerOrderDeliveryDto
            {                 
                Id = Guid.NewGuid(),
                CustomerOrderId = customerOrderId,
                AddressId = new Guid(entityToCreate.Order.addressId),
                CreatedDT = DateTime.UtcNow,
                CreatedBy = entityToCreate.UserName
            };

            _customerOrderDelivery.Insert(customerOrderDelivery);

            return customerOrderDelivery.Id;
        }
        private void CreatecustomerDeliveryStatus(Guid orderId)
        {
            var customerDeliveryStatus = new CustomerDeliveryStatusDto
            {
                Id = Guid.NewGuid(),
                OrderId = orderId,
                OrderStatusId = new Guid(eOrderStatus.Pending),
                CreatedDT = DateTime.UtcNow,
                CreatedBy = entityToCreate.UserName
            };

            _customerDeliveryStatus.Insert(customerDeliveryStatus);          
        }
        private OrderedSummaryDto OrderSummary(Guid orderId)
        {
            var orderedSummaryDto = new OrderedSummaryDto
            {
                AmountPaid = entityToCreate.Order.paid.ToDecimal(),
                Balance = entityToCreate.Order.balance.ToDecimal(),
                DeliveryCost = entityToCreate.Order.deliveryCost.ToDecimal(),
                DiscountRate = entityToCreate.Order.discount.ToDecimal(),
                TaxRate = entityToCreate.Order.tax.ToDecimal(),             
                Total = entityToCreate.Order.total.ToDecimal(),
                TotalDiscount = entityToCreate.Order.totalDiscount.ToDecimal(),
                TotalTax = entityToCreate.Order.totalTax.ToDecimal(),
                OrderId = orderId,
                Note = entityToCreate.Note,
                OrderStatusId = new Guid(eOrderStatus.Pending.ToUpper()),
                OrderTypeId = new Guid(eOrderType.Delivery),
                PaymentMethodId = new Guid(ePaymentMethod.CARD),
                orderedItems = MapItem(),
                IsProcessed = false
            };

            return orderedSummaryDto;
        }
        private List<OrderedItemDto> MapItem()
        {
            var items = new List<OrderedItemDto>();
            var baskets = entityToCreate.Baskets;

            foreach (var item in baskets)
            {
                items.Add(
                    new
                 OrderedItemDto
                    {
                        AddonIds = item.addonIds,
                        AddonItems = item.addonNames,
                        IsProcessed = false,
                        MenuId = new Guid(item.menuId),
                        Name = item.menu,
                        Price = item.price.ToDecimal(),
                        Quantity = item.quantity
                    });
            }

            return items;
        }
        private void ExecuteOrder(Guid orderId)
        {
            var customerOrderId = CreateCustomerOrder(orderId);

            CreateCustomerOrderDelivery(customerOrderId);
            CreatecustomerDeliveryStatus(orderId);

           _orderCommand.TenantId = new Guid(entityToCreate.ExternalId);
           _orderCommand.OrderedSummary = OrderSummary(orderId);
           _orderCommand.CreatedBy = entityToCreate.UserName;
           _orderCommand.CreatedDt = DateTime.UtcNow;

           _orderCommand.Execute();
        }

        #endregion

    }
}