using Stripe;
using Suftnet.Cos.DataAccess;
using Suftnet.Cos.Stripe;

namespace Suftnet.Cos.Web
{  
    public class Charge
    {
        public string StripePublishableKey { get; set; }       
        public string StripeToken { get; set; }
        public string Email { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string Processor { get; set; }
        public int ProductId { get; set; }
        public string Product { get; set; }
        public int PlanId { get; set; }
        public string StripeExternalId { get; set; }
        public int? DurationPeriod { get; set; }
        public string StripeCustomerId { get; set; }
        public string Reference { get; set; }
        public int IntervalCount { get; set; }
        public string Interval { get; set; }
        public bool IsCard { get; set; }
        //public ProductDetailsDto ProductDetails { get; set; }
        public CustomerPlan StripePlan { get; set; }
        public Card StripeCard { get; set; }


    }
}