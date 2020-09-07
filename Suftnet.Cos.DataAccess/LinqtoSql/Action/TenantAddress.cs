namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TenantAddress")]
    public partial class TenantAddress
    {       
        public Guid Id { get; set; }

        [StringLength(500)]
        public string AddressLine1 { get; set; }

        [StringLength(200)]
        public string AddressLine2 { get; set; }

        [StringLength(200)]
        public string AddressLine3 { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(15)]
        public string PostCode { get; set; }

        public bool? Active { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CreatedDT { get; set; }

        [StringLength(50)]
        public string Latitude { get; set; }

        [StringLength(50)]
        public string Logitude { get; set; }

        [StringLength(50)]
        public string County { get; set; }

        [StringLength(150)]
        public string CompleteAddress { get; set; }

        [StringLength(50)]
        public string Town { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamp { get; set; }
      
    }
}
