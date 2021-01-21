namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   
    public partial class CustomerOrderNotificationDto :Base2Dto
    {   
        public Guid CustomerId { get; set; }

        [Required]
        [StringLength(200)]
        public string Messages { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedAt { get; set; }
    }
}
