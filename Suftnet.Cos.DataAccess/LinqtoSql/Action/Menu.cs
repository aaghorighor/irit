namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
       
        public Guid Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public Guid CategoryId { get; set; }

        public Guid UnitId { get; set; }      

        public decimal Price { get; set; }

        [StringLength(3000)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDt { get; set; }

        public bool Active { get; set; }
    
        public int Quantity { get; set; }

        public int SubstractId { get; set; }

        public int? CutOff { get; set; }

        public bool? IsKitchen { get; set; }

        [StringLength(150)]
        public string ImageUrl { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamp { get; set; }
        public Guid TenantId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Categories { get; set; }        
        [ForeignKey("UnitId")]
        public virtual Unit Units { get; set; }

    }
}
