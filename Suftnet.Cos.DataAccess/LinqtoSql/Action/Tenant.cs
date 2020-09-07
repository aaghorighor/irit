namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tenant")]
    public partial class Tenant
    {      
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public Guid AddressId { get; set; }

        [StringLength(20)]
        public string Mobile { get; set; }

        [StringLength(20)]
        public string Telephone { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDt { get; set; }

        public Guid StatusId { get; set; }

        [StringLength(200)]
        public string CustomerStripeId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime ExpirationDate { get; set; }

        public int? CurrencyId { get; set; }

        public bool? Startup { get; set; }

        [StringLength(250)]
        public string WebsiteUrl { get; set; }

        public bool? IsExpired { get; set; }

        [StringLength(50)]
        public string PlanTypeId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? StartDate { get; set; }

        [StringLength(50)]
        public string LogoUrl { get; set; }

        public bool? Publish { get; set; }

        [StringLength(200)]
        public string StripePublishableKey { get; set; }

        [StringLength(200)]
        public string StripeSecretKey { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] TimeStamp { get; set; }

        public int? SubscriptionId { get; set; }

        [StringLength(50)]
        public string CurrencyCode { get; set; }

        [StringLength(50)]
        public string BackgroundUrl { get; set; }       
    }
}
