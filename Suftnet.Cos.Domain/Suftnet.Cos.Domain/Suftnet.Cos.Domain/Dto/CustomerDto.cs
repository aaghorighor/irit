namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
  
   
    public class CustomerDto : Base2Dto
    {             

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Mobile { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        public bool Active { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedAt { get; set; }
    
        [StringLength(100)]
        public string Serial { get; set; }

        [StringLength(100)]
        public string DeviceId { get; set; }

        [StringLength(50)]
        public string StripeCustomerId { get; set; }

    }

    public class CreateCustomerDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Mobile { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }       

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedAt { get; set; }

        [StringLength(100)]
        public string Serial { get; set; }

        [StringLength(100)]
        public string DeviceId { get; set; }
        [Required]      
        public Guid ExternalId { get; set; }
        [Required]
        [StringLength(50)]
        public string AppCode { get; set; }

    }

    public class UpdateCustomerDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Mobile { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]  
        public Guid ExternalId { get; set; }      

    }

    public class UpdateFcmTokenDto
    {
        [Required]
        public Guid CustomerId { get; set; }        
        [Required]
        public Guid ExternalId { get; set; }
        [Required]
        public string FcmToken { get; set; }

    }
}
