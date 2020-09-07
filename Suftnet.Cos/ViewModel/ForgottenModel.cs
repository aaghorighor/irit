namespace Suftnet.Cos.Web
{
    using System.ComponentModel.DataAnnotations;

    public class ForgottenModel
    {
        [Required()]
        [StringLength(50)]
        [EmailAddress(ErrorMessage ="Please enter a valid email address")]
        public string Email { get; set; }       
        public string Status { get; set; }
    }

    public class ResetPasswordModel
    {
        [Required()]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        public string ResetToken { get; set; }
        [Required]
        [StringLength(20)]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}
