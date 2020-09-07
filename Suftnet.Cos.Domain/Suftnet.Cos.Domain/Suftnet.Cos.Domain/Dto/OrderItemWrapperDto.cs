namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;

    public class OrderItemWrapperDto
    {
        public OrderItemWrapperDto()
        {
            ItemOrder = new List<OrderItemDto>();
        }

        public List<OrderItemDto> ItemOrder { get; set; }
        public int OrderId { get; set; }             
        public int PaymentMethodId { get; set; }
        public bool PrintReceipt { get; set; }      
        public int OrderTypeId { get; set; }
        public int OrderStatusId { get; set; }
        public DateTime CreatedDT  { get;  set; }
        public string CreatedBy { get; set; }
        public decimal Amount { get; set; }   
        public decimal Tax { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal Total { get; set; }
        public decimal GrandTotal { get; set; } 
    }
}
