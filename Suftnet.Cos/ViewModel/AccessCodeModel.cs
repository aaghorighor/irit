namespace Suftnet.Cos.Web
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AccessCodeModel
    {
        [Required()]
        [StringLength(50)]
        public string EmailAddress { get; set; }
        [Required()]
        [StringLength(6)]
        public string Otp { get; set; }
        [Required()]       
        public int AppCode { get; set; }       

    }
}
