namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;

    public class OrderDetailWrapperDto
    {
        public OrderDetailWrapperDto()
        {
            OrderDetail = new List<OrderDetailDto>();
        }

        public List<OrderDetailDto> OrderDetail { get; set; }
        public PaymentDto Payment { get; set; }
        public OrderDto Order { get; set; }
        public GlobalDto Settings { get; set; }
        public Guid OrderId { get; set; }
        public string OrderType { get; set; }
        public Guid OrderTypeId { get; set; }
        public string Note { get; set; }
        public bool IsKitchenReceipt { get; set; }
        public bool IsPrintReceipt { get; set; }
        public string Table { get; set; }
        public List<CommonDto> OrderStatus { get; set; }
        public List<CommonDto> PaymentMethod { get; set; }
    }

    public class KitchenAdapter
    {
        public KitchenAdapter()
        {
            KitchenBasket = new List<KitchenBasketDto>();
        }

        public List<KitchenBasketDto> KitchenBasket { get; set; }
        public Guid OrderId { get; set; }
        public Guid OrderTypeId { get; set; }
        public string OrderType { get; set; }
        public string Note { get; set; }
        public string Table { get; set; }
        public DateTime CreatedDT { get; set; }
        public string InTime
        {
            get
            {
                return this.CreatedDT.ToShortTimeString();
            }
        }
    }
}


