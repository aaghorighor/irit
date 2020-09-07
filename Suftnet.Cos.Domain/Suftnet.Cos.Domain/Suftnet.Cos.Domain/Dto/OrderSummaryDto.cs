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
        public int OrderId { get; set; }             
        public int PaymentMethodId { get; set; }    
        public int OrderStatusId { get; set; }
        public int OrderTypeId { get; set; }
        public string Note { get; set; }
        public bool IsProcessed { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal DeliveryCost { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TaxRate { get; set; }
        public int TableId { get; set; }    
        public decimal AmountPaid { get; set; }
        public decimal Balance { get; set; }
        public decimal Total { get; set; }
        public string IpAddress { get; set; }
        public bool IsPrintReceipt { get; set; }
        public bool IsKitchenReceipt { get; set; }
        public List<OrderedItemDto> orderedItems { get; set; }
    }
}
