namespace Suftnet.Cos.Web.ViewModel
{
    using System.ComponentModel.DataAnnotations;
    public class CheckoutModel
    {
        [Required(ErrorMessage = "Church Name is required")]
        [StringLength(200, ErrorMessage = "Church Name must be between 5 and 200 characters", MinimumLength = 5)]
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, ErrorMessage = "Email must be between 5 and 50 characters", MinimumLength = 5)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mobile is required")]
        [StringLength(20, ErrorMessage = "Mobile must be between 10 and 20 characters", MinimumLength = 10)]
        public string Mobile { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, ErrorMessage = "Password must be between 6 and 20 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Street address is required")]
        [StringLength(150, ErrorMessage = "Street address must be between 10 and 150 characters", MinimumLength = 10)]
        public string CompleteAddress { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string County { get; set; }
        [Required(ErrorMessage = "Country is required")]
        [StringLength(50)]
        public string Country { get; set; }
        public string Logitude { get; set; }
        public string Latitude { get; set; }
        [Required(ErrorMessage = "PostCode is required")]
        [StringLength(50)]
        public string PostCode { get; set; }
        [Required(ErrorMessage = "Town is required")]
        [StringLength(50)]
        public string Town { get; set; }
        public string CustomerStripeId { get; set; }
        public int PlanId { get; set; }
        [Required(ErrorMessage = "Plan Type is required")]
        [StringLength(50)]
        public string PlanTypeId { get; set; }
        public string StripeToken { get; set; }
        public string StripeExternalId { get; set; }
        [Required(ErrorMessage = "Denomination Id is required")]   
        public int DenominationId { get; set; }
        public string Website { get; set; }
        public string Terms { get; set; }
        public string TenantId { get; set; }      

    }
}