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
        public int Id { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDT { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public int TenantId { get; set; }

        [Required]
        [StringLength(2000)]
        public string Body { get; set; }

        public int MessageTypeId { get; set; }

        public int StatusId { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamp { get; set; }     
    }
}
