namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Chapter")]
    public partial class Chapter
    {
        public int Id { get; set; }
        [Required]      
        public string Description { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDT { get; set; }
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        public int SectionId { get; set; }
        [Required]
        public int SubSectionId { get; set; }
        public bool Publish { get; set; }
        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamp { get; set; }
        [StringLength(50)]
        public string ImageUrl { get; set; }

    }
}
