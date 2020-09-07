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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tenant()
        {
           
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(3000)]
        public string Description { get; set; }
        public int AddressId { get; set; }
        [StringLength(20)]
        public string Mobile { get; set; }
        [StringLength(20)]
        public string Telephone { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }      
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDT { get; set; }
        public int StatusId { get; set; }
        [StringLength(200)]
        public string CustomerStripeId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime? ExpirationDate { get; set; }
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
        [StringLength(150)]
        public string SubscriptionId { get; set; }
        [Column(TypeName = "timestamp")]
        [MaxLength(8)]
        [Timestamp]
        public byte[] TimeStamp { get; set; }
        [StringLength(50)]
        public string CurrencyCode { get; set; }
        public string BackgroundUrl { get; set; }      

    }
}
