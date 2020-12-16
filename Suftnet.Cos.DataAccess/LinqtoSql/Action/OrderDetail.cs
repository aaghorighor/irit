namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }

        public Guid MenuId { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Column(TypeName = "money")]
        public decimal LineTotal { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDt { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public Guid OrderId { get; set; }

        public double? Discount { get; set; }

        public double? DiscountRate { get; set; }

        public double Tax { get; set; }

        public double? TaxRate { get; set; }

        public bool? IsProcessed { get; set; }

        [Required]
        [StringLength(550)]
        public string ItemName { get; set; }

        public bool? IsKitchen { get; set; }

        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamp { get; set; }
        public DateTime? UpdateDt { get; set; }
        [StringLength(50)]
        public string UpdateBy { get; set; }
        [StringLength(50)]
        public string AddonIds { get; set; }
        [StringLength(50)]
        public string AddonNames { get; set; }
    }
}
