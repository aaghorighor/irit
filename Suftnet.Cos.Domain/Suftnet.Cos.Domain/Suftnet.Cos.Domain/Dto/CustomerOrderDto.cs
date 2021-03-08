namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   
    public partial class CustomerOrderDto :OrderDto
    {       
        public Guid CustomerId { get; set; }
        public Guid OrderId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedAt { get; set; }

        public new string OrderType
        {
            get
            {
                return "Delivery";
            }
        }
    }

    public partial class MobileCustomerOrderDto 
    {
        public Guid OrderTypeId { get; set; }
        public string OrderType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string PaymentStatus { get; set; }
        public decimal? TaxRate { get; set; }
        public decimal? DeliveryCost { get; set; }
        public decimal? Payment { get; set; }
        public decimal? TotalDiscount { get; set; }
        public decimal? DiscountRate { get; set; }
        public decimal? TotalTax { get; set; }
        public decimal? Total { get; set; }
        public decimal? GrandTotal { get; set; }
        public decimal? Balance { get; set; }
        public string Status { get; set; }
        public Guid CustomerId { get; set; }
        public Guid AddressId { get; set; }
        public string CompletedAddress { get; set; }
        public Guid StatusId { get; set; }
        public Guid OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string OrderDate
        {
            get
            {
                return this.CreatedAt.ToLongDateString();
            }
        }
      
    }
}
