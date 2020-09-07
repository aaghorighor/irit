namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Addon")]
    public partial class Addon
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public Guid MenuId { get; set; }
        [Column(TypeName = "money")]
        public decimal? Price { get; set; }
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDt { get; set; }
        public bool Active { get; set; }
        public Guid AddonTypeId { get; set; }
       
    }
}
