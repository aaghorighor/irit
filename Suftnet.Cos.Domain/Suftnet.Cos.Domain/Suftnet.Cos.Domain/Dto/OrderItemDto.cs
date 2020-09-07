namespace Suftnet.Cos.DataAccess
{
    using System;

    public class OrderItemDto
    {       
        public int MenuId { get; set; }             
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public double Tax { get; set; }  
        public decimal Price { get; set; }               
    }
}
