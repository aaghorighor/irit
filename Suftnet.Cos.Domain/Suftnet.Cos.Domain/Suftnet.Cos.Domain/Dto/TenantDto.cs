namespace Suftnet.Cos.DataAccess
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    public class TenantDto : TenantAddressDto
    {    
      [Required()]
      [StringLength(50)]
      public string Name { get; set; }       
      public string Mobile { get; set; }
      [Required()]
      [StringLength(20)]
      public string Telephone { get; set; }
      [Required()]
      [EmailAddress]      
      public string Email { get; set; }
      [Required()]
      public string Description { get; set; }
      [Required()]
      public Guid StatusId { get; set; }
      public string Status { get; set; }
      [Required()]
      public string PlanTypeId { get; set; }    
      public DateTime ExpirationDate { get; set; }
      public DateTime? StartDate { get; set; }   
      public string SubscriptionId { get; set; }        
      public string ExpireDate
        {
            get
            {
                return this.ExpirationDate.ToShortDateString();
            }
        }
      public string CustomerStripeId { get; set; }        
      public int? CurrencyId { get; set; }      
      public string CurrencySymbol { get; set; }
      public bool? Startup { get; set; }       
      public string WebsiteUrl { get; set; }      
      public bool IsExpired { get; set; }
      public decimal Total { get; set; }  
      public string PlanType { get; set; }   
      public string LogoUrl { get; set; }
      public int? PhotoFlag { get; set; }    
      public bool? Publish { get; set; }   
      public string StripePublishableKey { get; set; }
      public string StripeSecretKey { get; set; }     
      public string CurrencyCode { get; set; }
      public string BackgroundUrl { get; set; }     
      public string AppCode { get; set; }
      public decimal? TaxRate { get; set; }
      public decimal? DiscountRate { get; set; }
      public decimal? DeliveryCost { get; set; }
    }

    public class MobileTenantDto
    {
        [JsonIgnore]
        public string Id { get; set; }
        [JsonIgnore]
        public Guid TenantId { get; set; }
        public string ExternalId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }       
        public string Telephone { get; set; }       
        public string Email { get; set; }      
        public string Description { get; set; }
        public string CurrencySymbol { get; set; }
        public string WebsiteUrl { get; set; }
        public string LogoUrl { get; set; }
        public string StripePublishableKey { get; set; }       
        public string County { get; set; }
        public string CompleteAddress { get; set; }
        public string Country { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string PostCode { get; set; }
        public string Town { get; set; }
        public bool Active { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public string Area { get; set; }
        public int AreaId { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public Guid CustomerId { get; set; }
        public TenantDto Tenant { get; set; }
    }

    public class TenantAdapter
    {
        [IgnoreDataMember]
        public int Count { get; set; }      
        public IList<TenantDto> TenantDto { get; set; }
    }
}
