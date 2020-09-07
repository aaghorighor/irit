namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;

    public class OrderWrapperDto 
    {
        public OrderWrapperDto()
        {
            Menu = new List<MenuDto>();
            Order = new List<OrderDto>();          
        }

        public List<MenuDto> Menu { get; set; }  
        public List<OrderDto> Order { get; set; }      
        
    }
}
