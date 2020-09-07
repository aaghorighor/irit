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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
            OrderPayments = new HashSet<OrderPayment>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Reference { get; set; }

        public int TableId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDT { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "money")]
        public decimal Total { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalTax { get; set; }

        [Column(TypeName = "money")]
        public decimal TotalDiscount { get; set; }

        [Column(TypeName = "money")]
        public decimal GrandTotal { get; set; }

        public int StatusId { get; set; }

        public int OrderTypeId { get; set; }

        [Column(TypeName = "money")]
        public decimal Payment { get; set; }

        [StringLength(10)]
        public string Time { get; set; }

        public int? NumberOfGuest { get; set; }

        [Column(TypeName = "money")]
        public decimal? Balance { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(500)]
        public string DeliveryAddress { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        public int TenantId { get; set; }

        public virtual Table Table { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderPayment> OrderPayments { get; set; }
    }
}
