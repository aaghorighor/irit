namespace Suftnet.Cos.DataAccess.Action
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserAccount")]
    public partial class UserAccount
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }
        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDt { get; set; }
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }
        public Guid TenantId { get; set; }
        [ForeignKey("TenantId")]
        public virtual Tenant Tenants { get; set; }
        [Required]
        public int AppCode { get; set; }
        [Required]
        [StringLength(50)]
        public string EmailAddress { get; set; }        

    }
}
