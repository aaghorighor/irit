namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Web.ViewModel;
    using System;
    using System.Collections.Generic;
    using Suftnet.Cos.Extension;
    using Suftnet.Cos.Common;

    public class UpdateOrderCommand : IUpdateOrderCommand
    {
        private readonly IOrderCommand _orderCommand;

        public UpdateOrderCommand(
           IOrderCommand orderCommand)
        {
            _orderCommand = orderCommand;
        }               
              
        public OrderAdapter OrderAdapter { get; set; }     

        public void Execute()
        {
            _orderCommand.TenantId = new Guid(OrderAdapter.externalId);
            _orderCommand.OrderedSummary = MapOrder();
            _orderCommand.CreatedBy = OrderAdapter.userName;
            _orderCommand.CreatedDt = DateTime.UtcNow;
            _orderCommand.Execute();
        }  
        
        private OrderedSummaryDto MapOrder()
        {
            var orderedSummaryDto = new OrderedSummaryDto
            {
                AmountPaid = OrderAdapter.order.paid.ToDecimal(),
                Balance = OrderAdapter.order.balance.ToDecimal(),
                DeliveryCost = 0,
                DiscountRate  = OrderAdapter.order.discount.ToDecimal(),
                TaxRate = OrderAdapter.order.tax.ToDecimal(),
                TableId = new Guid(OrderAdapter.order.tableId),
                Total = OrderAdapter.order.total.ToDecimal(),
                TotalDiscount = OrderAdapter.order.totalDiscount.ToDecimal(),
                TotalTax = OrderAdapter.order.totalTax.ToDecimal(),
                OrderId = new Guid( OrderAdapter.order.externalId),
                Note = string.Empty,
                OrderStatusId = new Guid(eOrderStatus.Occupied),
                OrderTypeId = new Guid(eOrderType.DineIn),
                PaymentMethodId = new Guid(ePaymentMethod.CASH),
                orderedItems = MapItem(),
                IsProcessed = false
            };

            return orderedSummaryDto;
        }

        private List<OrderedItemDto> MapItem()
        {
            var items = new List<OrderedItemDto>();
            var baskets = OrderAdapter.baskets;

            foreach(var item in baskets)
            {
                items.Add(
                    new 
                 OrderedItemDto
                 { 
                        AddonIds = item.addonIds,
                        AddonNames= item.addons, 
                        IsProcessed = false, 
                        MenuId = new Guid(item.menuId),
                        Name = item.menu, 
                        Price = item.price.ToDecimal(),
                    });}

            return items;
        }

    }
}