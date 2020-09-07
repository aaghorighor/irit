namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SubTopic")]
    public partial class SubTopic
    {
        [Required]
        public string Description { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public int TopicId { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamp { get; set; }

        [StringLength(50)]
        public string ImageUrl { get; set; }

        public int IndexNo { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        public int Id { get; set; }
    }
}
