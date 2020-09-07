namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Table")]
    public partial class Table
    {
        public Guid Id { get; set; }

        public int Size { get; set; }

        [Required]
        [StringLength(20)]
        public string Number { get; set; }
        [StringLength(20)]
        public string TimeIn { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDt { get; set; }
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }
        public Guid? OrderId { get; set; }
        public bool Active { get; set; }
        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamp { get; set; }
        public Guid TenantId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDt { get; set; }
        [StringLength(50)]
        public string UpdateBy { get; set; }

    }
}
