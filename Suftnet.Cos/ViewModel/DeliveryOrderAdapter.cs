namespace Suftnet.Cos.Web.ViewModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class DeliveryBasket
    {
        public string addonIds { get; set; }
        public string addons { get; set; }
        public int id { get; set; }
        public string menu { get; set; }
        public string menuId { get; set; }   
        public double price { get; set; }
    }

    public class DeliverOrder
    {
        public double balance { get; set; }
        public double discount { get; set; }
        public string externalId { get; set; }
        public double grandTotal { get; set; }
        public int id { get; set; }
        public double paid { get; set; }
        public string addressId { get; set; }
        public string customerId { get; set; }
        public double tax { get; set; }
        public double total { get; set; }
        public double totalDiscount { get; set; }
        public double totalTax { get; set; }
        public double deliveryCost { get; set; }
    }

    public class DeliveryOrderAdapter
    {
        public IList<DeliveryBasket> Baskets { get; set; }
        [Required]
        [StringLength(50)]
        public string ExternalId { get; set; }
        public DeliverOrder Order { get; set; }
        public string UserName { get; set; }
        public string Note { get; set; }
    }

}