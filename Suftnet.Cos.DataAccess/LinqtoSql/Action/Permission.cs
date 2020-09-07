namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Permission")]
    public partial class Permission
    {
        public Guid Id { get; set; }

        public int ViewId { get; set; }

        public int Create { get; set; }

        public int Edit { get; set; }

        public int Remove { get; set; }

        public int Get { get; set; }

        public int GetAll { get; set; }

        public int? Custom { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDt { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamp { get; set; }
        [Required]
        [StringLength(150)]
        public string UserId { get; set; }
       
    }
}
