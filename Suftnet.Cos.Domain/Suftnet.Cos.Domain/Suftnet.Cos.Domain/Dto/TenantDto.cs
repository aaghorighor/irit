namespace Suftnet.Cos.DataAccess
{
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
      public decimal DeliveryRate { get; set; }    
      public string DeliveryUnitId { get; set; }       
      public string DeliveryLimitNote { get; set; }
      public bool? IsFlatRate { get; set; }
      public decimal? FlatRate { get; set; }
      public string AppCode { get; set; }
    }

    public class TenantAdapter
    {
        [IgnoreDataMember]
        public int Count { get; set; }      
        public IList<TenantDto> TenantDto { get; set; }
    }
}
