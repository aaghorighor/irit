using System.ComponentModel.DataAnnotations;

namespace Suftnet.Cos.Web.ViewModel
{
    public class Param
    {
        [Required]
        [StringLength(50)]
        public string ExternalId { get; set; }
    }
}