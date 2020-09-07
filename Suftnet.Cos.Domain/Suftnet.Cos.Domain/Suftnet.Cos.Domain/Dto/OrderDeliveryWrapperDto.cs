namespace Suftnet.Cos.DataAccess
{
    using System.Collections.Generic;
   
    public class OrderDeliveryWrapperDto
    {
        public OrderDeliveryWrapperDto()
        {
            OrderDetail = new List<OrderDetailDto>();
        }
      
        public List<OrderDetailDto> OrderDetail { get; set; }

        public OrderDto Order { get; set; }       
       
    }
}


