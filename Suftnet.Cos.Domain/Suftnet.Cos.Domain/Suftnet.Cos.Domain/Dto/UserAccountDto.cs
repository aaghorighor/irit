namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.Contracts;

    public class UserAccountDto
    {
        public string UserId { get; set; }
        public string Id { get; set; }
        public string UserName { get; set; }
        public bool Active { get; set; }      
        public string Email { get; set; }
        public string Area { get; set; }
        public string Password { get; set; }
        public DateTime LastLoginDate { get; set; }      
        public string FirstName { get; set; }     
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public string Phone { get; set; }
        [Required()]
        public int AreaId { get; set; }
        public Guid TenantId { get; set; }
        public bool ChangePassword { get; set; }
        public string TenantName { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool? IsExpired { get; set; }
        public string FullName { get {
                return FirstName + " " + LastName;
            }
        }       
        public string CompleteAddress { get; set; }
        public decimal? DeliveryCost { get; set; }
        public decimal? TaxRate { get; set; }
        public decimal? DiscountRate { get; set; }
        public string DeliveryUnit { get; set; }
        public string CurrencyCode { get; set; }        
        public string TenantEmail { get; set; }
        public string TenantMobile { get; set; }
        public TenantDto TenantDto { get; set; }
        public string AppCode { get; set; }       
        public string UserCode { get; set; }
    }

    public class MobileUserDto
    {
        public string Id { get; set; }
        [Required()]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required()]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required()]
        [StringLength(50)]
        public string Phone { get; set; }

    }
}
