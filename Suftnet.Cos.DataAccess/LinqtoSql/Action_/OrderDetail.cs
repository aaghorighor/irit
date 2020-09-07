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
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int MenuId { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Column(TypeName = "money")]
        public decimal LineTotal { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDT { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public int OrderId { get; set; }

        public double? Discount { get; set; }

        public double Tax { get; set; }

        public bool? IsProcessed { get; set; }

        [StringLength(550)]
        public string ItemName { get; set; }

        public bool? IsKitchen { get; set; }

        public virtual Order Order { get; set; }
    }
}
