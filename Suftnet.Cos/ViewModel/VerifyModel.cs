namespace Suftnet.Cos.Web
{
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
}
