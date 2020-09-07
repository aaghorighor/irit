namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MobilePermission")]
    public partial class MobilePermission
    {
        public int Id { get; set; }

        public int PermissionId { get; set; }
              
        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDT { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamp { get; set; }

        [StringLength(150)]
        public string IdentityId { get; set; }       
       
    }
}
