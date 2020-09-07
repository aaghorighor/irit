namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tutorial")]
    public partial class Tutorial
    {
        public int Id { get; set; }

        public int ActionId { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int Sequency { get; set; }

        public int TutorialGroupId { get; set; }

        public bool? Active { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDT { get; set; }

        [Required]
        [StringLength(50)]
        public string VideoId { get; set; }

        [StringLength(150)]
        public string VideoUrl { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamp { get; set; }
       
    }
}
