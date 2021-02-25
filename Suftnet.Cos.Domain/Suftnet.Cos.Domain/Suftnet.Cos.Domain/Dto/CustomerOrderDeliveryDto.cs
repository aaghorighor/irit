namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
  
    public partial class CustomerOrderDeliveryDto :CustomerAddressDto
    {
        public new Guid AddressId { get; set; }
        public Guid CustomerOrderId { get; set; }       
       
    }
}
