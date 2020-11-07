namespace Suftnet.Cos.DataAccess.Identity
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Security.Claims;

    using System.ComponentModel.DataAnnotations;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ApplicationUser : IdentityUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ApplicationUser()
        {
           
        }

        public bool Active { get; set; }
        [StringLength(50)]
        public string ImageUrl { get; set; }
        [Required]
        public int AreaId { get; set; }
        public string Area { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string OTP { get; set; }
        [NotMapped]
        public Guid TenantId { get; set; }         
    }
}
