namespace Suftnet.Cos.Web
{
    using System.ComponentModel.DataAnnotations;

    public class LoginModel
    {
        [Required()]
        [StringLength(50)]
        public string Username { get; set; }
        [Required()]
        [StringLength(20)]
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}
