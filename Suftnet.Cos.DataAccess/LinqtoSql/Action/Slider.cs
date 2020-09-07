namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Slider")]
    public partial class Slider
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        [Required]
        [StringLength(550)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string ImageUrl { get; set; }

        public bool Publish { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public int? SliderTypeId { get; set; }

        [StringLength(50)]
        public string Alt { get; set; }

        public virtual Common Common { get; set; }
    }
}
