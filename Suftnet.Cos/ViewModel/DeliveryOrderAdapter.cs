namespace Suftnet.Cos.Web.ViewModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class DeliveryBasket
    {
        public string addonIds { get; set; }
        public string addonNames { get; set; }
        public int id { get; set; }
        public string menu { get; set; }
        public string menuId { get; set; }   
        public double price { get; set; }
        public int quantity { get; set; }
    }

    public class DeliverOrder
    {
        public decimal Balance { get; set; }
        public decimal DiscountRate { get; set; }
        public string ExternalId { get; set; }
        public decimal GrandTotal { get; set; }
        public int id { get; set; }
        public decimal Paid { get; set; }
        public string AddressId { get; set; }
        public string CustomerId { get; set; }
        public decimal TaxRate { get; set; }
        public decimal Total { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal DeliveryCost { get; set; }
    }

    public class DeliveryOrderAdapter
    {
        public IList<DeliveryBasket> Baskets { get; set; }
        [Required]
        [StringLength(50)]
        public string ExternalId { get; set; }
        public DeliverOrder Order { get; set; }
        public Guid OrderId { get; set; }
        public string UserName { get; set; }
        public string Note { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string SourceToken { get; set; }        
        public string Mobile { get; set; }
        [Required]
        [StringLength(50)]
        public string CustomerId { get; set; }
        [Required]
        [StringLength(500)]
        public string FcmToken { get; set; }

    }

}