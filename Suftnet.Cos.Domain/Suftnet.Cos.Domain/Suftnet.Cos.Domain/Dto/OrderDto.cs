namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class OrderDto : Base2Dto
    {                  
        public Guid TableId { get; set; }
        public string Table { get; set; }    
        public Guid StatusId { get; set; }        
        public string Status { get; set; }    
        public string Note { get; set; }
        public Guid OrderTypeId { get; set; }
        public string OrderType { get; set; }
        public decimal? TaxRate { get; set; }
        public decimal? DiscountRate { get; set; }
        public decimal? Payment { get; set; }
        public decimal? TotalDiscount { get; set; }
        public decimal? TotalTax { get; set; }
        public decimal? Total { get; set; }
        public decimal? GrandTotal { get; set; }
        public decimal? Balance { get; set; }       
        public string Time { get; set; }
        public int? ExpectedGuest { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool ChangeTable { get; set; }
        public  string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public string DeliveryAddress { get; set; }
        public string Mobile { get; set; }
        public DateTime? StartDt { get; set; }
        public string StartOn
        {
            get
            {
                return this.StartDt?.ToShortDateString();
            }
        }

        public string Email { get; set; }      
    }
    public class CartOrderDto 
    {
        public OrderDto Order { get; set; }
        public List<OrderDetailDto> Carts { get; set; }
    }
}
