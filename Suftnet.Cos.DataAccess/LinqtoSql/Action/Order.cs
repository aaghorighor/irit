namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {       
        public Guid Id { get; set; }       
        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDt { get; set; }
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "money")]
        public decimal? Total { get; set; }
        [Column(TypeName = "money")]
        public decimal? TotalTax { get; set; }
        [Column(TypeName = "money")]
        public decimal? TotalDiscount { get; set; }
        [Column(TypeName = "money")]
        public decimal? GrandTotal { get; set; }
        [Column(TypeName = "money")]
        public decimal? Payment { get; set; }
        [StringLength(10)]
        public string Time { get; set; }
        public int? ExpectedGuest { get; set; }
        [Column(TypeName = "money")]
        public decimal? Balance { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Mobile { get; set; }
        public Guid StatusId { get; set; }
        public Guid OrderTypeId { get; set; }
        public Guid TableId { get; set; }
        public Guid TenantId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? StartDt { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDt { get; set; }      
        [StringLength(50)]
        public string UpdateBy { get; set; }
        [StringLength(1000)]
        public string Note { get; set; }

    }
}
