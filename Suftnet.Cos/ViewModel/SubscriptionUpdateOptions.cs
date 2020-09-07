namespace Suftnet.Cos.Web
{
    using DataAccess;
    using global::Stripe;
    using Stripe;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SubscriptionUpdateOptions : CardOptions
    {
        public SubscriptionUpdateOptions()
        {
            Invoices = new List<CustomerInvoice>();
        }

        [Required()]
        [StringLength(100)]
        public string subscriptionid { get; set; }
        [Required()]
        [StringLength(100)]
        public string PlanId { get; set; }
        public string ProductId { get; set; }
        public int PaymentyStatus { get; set; }
        public bool IsTenant { get; set; }
        public bool IsCard { get; set; }    
        public GlobalDto Settings { get; set; }
        public PlanFeatureAdapter PlanFeatureAdapter { get;set;}
        public TenantDto Tenant { get; set; }
        public List<CustomerInvoice> Invoices { get; set; }       
        public IEnumerable<Subscription> StripeSubscription { get; set; }       
    }
}
