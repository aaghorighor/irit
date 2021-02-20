namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Notification")]
    public partial class Notification
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Subject { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public Guid TenantId { get; set; }

        [Required]
        [StringLength(2000)]
        public string Body { get; set; }
      
        public int StatusId { get; set; }
    }
}
