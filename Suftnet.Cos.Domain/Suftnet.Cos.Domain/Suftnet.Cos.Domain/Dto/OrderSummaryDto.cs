namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;

    public class OrderedSummaryDto
    {   
        public OrderedSummaryDto()
        {
            orderedItems = new List<OrderedItemDto>();
        }
        public Guid OrderId { get; set; }             
        public Guid PaymentMethodId { get; set; }
        public Guid AccountTypeId { get; set; }
        public Guid OrderStatusId { get; set; }
        public Guid OrderTypeId { get; set; }
        public string Note { get; set; }
        public bool IsProcessed { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal DeliveryCost { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TaxRate { get; set; }
        public Guid TableId { get; set; }    
        public decimal AmountPaid { get; set; }
        public decimal Balance { get; set; }
        public decimal Total { get; set; }     
        public List<OrderedItemDto> orderedItems { get; set; }
    }
}
