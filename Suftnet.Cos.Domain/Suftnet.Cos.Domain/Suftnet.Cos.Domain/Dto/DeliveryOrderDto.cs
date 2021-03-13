namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   
    public class DeliveryOrderDto : Base2Dto
    {      
        public string UserId { get; set; }
        [Required]
        public Guid OrderId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedAt { get; set; }
      
    }

    public class UpdateDeliveryStatusDto 
    {
        [Required]
        public Guid StatusId { get; set; }
        [Required]
        public Guid OrderId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedAt { get; set; }

    }


}
