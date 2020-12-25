﻿namespace Suftnet.Cos.Web.ViewModel
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Basket
    {
        public string addonIds { get; set; }
        public string addons { get; set; }
        public int id { get; set; }
        public string menu { get; set; }
        public string menuId { get; set; }
        public string orderId { get; set; }
        public bool isProcessed { get; set; }
        public double price { get; set; }
    }

    public class Order
    {
        public double balance { get; set; }
        public double discount { get; set; }
        public string externalId { get; set; }
        public double grandTotal { get; set; }
        public int id { get; set; }
        public double paid { get; set; }
        public int tableFor { get; set; }
        public string tableId { get; set; }
        public double tax { get; set; }
        public double total { get; set; }
        public double totalDiscount { get; set; }
        public double totalTax { get; set; }
    }

    public class OrderAdapter
    {
        public IList<Basket> baskets { get; set; }
        [Required]
        [StringLength(50)]
        public string externalId { get; set; }
        public Order order { get; set; }
        public string userName { get; set; }
    }

    public class OrderDone
    {
        [Required]
        [StringLength(50)]
        public string externalId { get; set; }
        [Required]
        [StringLength(50)]
        public string userName { get; set; }
        [Required]
        [StringLength(50)]
        public string orderId { get; set; }
        public DateTime updateDate { get; set; }
    }

    public class CancelOrder
    {
        [Required]
        [StringLength(50)]
        public string externalId { get; set; }
        [Required]
        [StringLength(50)]
        public string userName { get; set; }
        [Required]
        [StringLength(50)]
        public string orderId { get; set; }
        [Required]
        [StringLength(50)]
        public string tableId { get; set; }
        public DateTime updateDate { get; set; }
    }
}