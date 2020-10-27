namespace Suftnet.Cos.Web.ViewModel
{
    using DataAccess;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TenantModel : TenantAddressDto
    {      
        [Required()]
        [StringLength(50)]
        public string Name { get; set; }       
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        [Required()]
        [EmailAddress]
        public string Email { get; set; }
        public string Description { get; set; }      
        public string WebsiteUrl { get; set; }
        public string Mission { get; set; }     
        public int? CurrencyId { get; set; }      
        public string LogoUrl { get; set; }
        public int? PhotoFlag { get; set; }     
        public bool? Publish { get; set; }       
        public string StripePublishableKey { get; set; }
        public string StripeSecretKey { get; set; }   
        public bool PushNotification { get; set; }
        public string CurrencyCode { get; set; }
        public string BackgroundUrl { get; set; }
        public decimal DeliveryRate { get; set; }
        public string DeliveryUnitId { get; set; }
        public string DeliveryLimitNote { get; set; }
        public bool? IsFlatRate { get; set; }
        public Guid StatusId { get; set; }
        public decimal? FlatRate { get; set; }

    }
}