namespace Suftnet.Cos.DataAccess
{
    using System;  
 
    public class OrderDetailDto : Base2Dto
    {     
        public Guid MenuId { get; set; }    
        public Guid CategoryId { get; set; }           
        public string Unit { get; set; }
        public double TaxRate { get; set; }
        public double? Discount { get; set; }
        public int Quantity { get; set; }    
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ItemName { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal Total { get; set; }    
        public Guid OrderId { get; set; }
        public bool? IsProcessed { get; set; }
        public bool? IsKitchen { get; set; }

        public decimal LineTotal {
            get {

                decimal total = (Quantity * Price);              
                return total;
             }
        }
    }
}
