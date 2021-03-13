namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Payment")]
    public partial class Payment
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDt { get; set; }
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        [StringLength(50)]
        public string Reference { get; set; }
        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamp { get; set; }
        public Guid PaymentMethodId { get; set; }
        public Guid AccountTypeId { get; set; }
        public Guid TenantId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdateDt { get; set; }      
        [StringLength(50)]
        public string UpdateBy { get; set; }

    }
}
