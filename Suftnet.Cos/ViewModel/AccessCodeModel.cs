namespace Suftnet.Cos.Web
{
    using System.ComponentModel.DataAnnotations;

    public class AccessCodeModel
    {
        [Required()]
        [StringLength(150)]
        public string Phone { get; set; }
        [Required()]
        [StringLength(150)]
        public string Code { get; set; }
        [Required()]
        [StringLength(150)]
        public string ExternalId { get; set; }       

    }
}
