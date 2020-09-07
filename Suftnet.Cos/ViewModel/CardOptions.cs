namespace Suftnet.Cos.Web
{
    using System.ComponentModel.DataAnnotations;

    public class CardOptions
    {
        [Required()]
        [StringLength(100)]
        public string customerid { get; set; }       
        [Required()]
        [StringLength(100)]
        public string cardid { get; set; }
        [Required()]
        [StringLength(100)]
        public string Name { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string stripeToken { get; set; }
        public string __RequestVerificationToken { get; set; }

    }
}
