namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerOrderNotification")]
    public partial class CustomerOrderNotification
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        [Required]
        [StringLength(200)]
        public string Messages { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }
    }
}
