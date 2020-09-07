namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Device")]
    public partial class Device
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string AppVersion { get; set; }

        [StringLength(50)]
        public string OsVersion { get; set; }

        [Required]
        [StringLength(50)]
        public string Serial { get; set; }

        [StringLength(50)]
        public string DeviceName { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDT { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public Guid TenantId { get; set; }

        [Required]
        [StringLength(250)]
        public string DeviceId { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamp { get; set; }
    
    }
}
