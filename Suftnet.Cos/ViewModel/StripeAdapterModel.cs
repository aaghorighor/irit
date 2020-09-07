namespace Suftnet.Cos.Web
{
    using global::Stripe;
    using Stripe;
    using Suftnet.Cos.DataAccess;
    using System.Collections.Generic;

    public class StripeAdapterModel
    {
        public List<Card> PaymentCards { get; set; }
        public Subscription Subscription { get; set; }
        public UserAccountDto UserAccount { get; set; }
        public GlobalDto Settings { get; set; }
        public TenantDto Tenant { get; set; }
        public SubscriptionPlan StripePlan { get; set; }
        public List<CustomerInvoice> Invoices { get; set; }
        public CustomerInvoice Invoice { get; set; }
        public PlanFeatureAdapter PlanFeatureAdapter { get; set; }
    }
}