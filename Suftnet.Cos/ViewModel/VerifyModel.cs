namespace Suftnet.Cos.Web
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class VerifyModel
    {
        [Required()]
        [StringLength(50)]
        public string Phone { get; set; }
        [Required()]
        [StringLength(50)]
        public string ExternalId { get; set; }
    }

    public class VerifyAppCode
    {
        [Required()]
        [StringLength(50)]
        public string EmailAddress { get; set; }
        [Required()]
        [StringLength(50)]
        public string AppCode { get; set; }
    }

    public class VerifyUser
    {
        [Required()]
        [StringLength(50)]
        public string ExternalId { get; set; }
        [Required()]
        [StringLength(10)]
        public string UserCode { get; set; }
    }
}
