namespace Suftnet.Cos.Stripe
{
    using System; 
    using System.Linq;

    using global::Stripe;
  
    public class SubscriptionProvider : ISubscriptionProvider
    {
        private readonly SubscriptionService _subscriptionService;      
        public SubscriptionProvider(string stripeProviderId)
        {
            this._subscriptionService = new SubscriptionService(stripeProviderId);
        }               
       
        public string SubscriptionId(string stripeCustomerId)
        {
            var subscriptions = (from o in this._subscriptionService.List()
                                 where o.CustomerId == stripeCustomerId select o).FirstOrDefault();
            return subscriptions != null ? subscriptions.Id : string.Empty;
        }            
        public Subscription Get(string subscriptionId)
        {           
            var subscription = this._subscriptionService.Get(subscriptionId);
            return subscription;
        }

        public Subscription GetSubscriptionByCustomerId(string stripeCustomerId)
        {                                 
            var subscription =  (from o in this._subscriptionService.List()
                    where o.CustomerId == stripeCustomerId
                    select o).FirstOrDefault();

            return subscription;
            }
        public DateTime CancelSubscription(string stripeCustomerId, bool cancelAtPeriodEnd)
        {
            var subscriptionId = this.SubscriptionId(stripeCustomerId);

            SubscriptionCancelOptions subscriptionCancelOptions = new SubscriptionCancelOptions
            {
                  Prorate = cancelAtPeriodEnd,
                  InvoiceNow = true                   
            };

            var subscription = this._subscriptionService.Cancel(subscriptionId, subscriptionCancelOptions);
            return cancelAtPeriodEnd == true ? subscription.CurrentPeriodEnd.Value : DateTime.UtcNow;

        }       
        public Subscription UpdateSubscription(string stripeCustomerId, string newPlanId, bool proRate)
        {
            var currentSubscription = this._subscriptionService.Get(this.SubscriptionId(stripeCustomerId));

            var subscriptionOptions = new SubscriptionUpdateOptions()
            {
                PlanId = newPlanId,
                Prorate = proRate
            };                

          return  _subscriptionService.Update(currentSubscription.Id, subscriptionOptions);
        }        
        public void UpdateSubscription(string stripeCustomerId, decimal taxPercent = 0)
        {
            var subscription = new SubscriptionUpdateOptions
            {
                TaxPercent = taxPercent
            };

            var subscriptionId = this.SubscriptionId(stripeCustomerId);

            _subscriptionService.Update(subscriptionId, subscription);
        }        
        public Subscription SubscribeUserNaturalMonth(string stripeCustomerId, string planId, DateTime? billingAnchorCycle, decimal? taxPercent)
        {
            Subscription stripeSubscription = _subscriptionService.Create
                (new SubscriptionCreateOptions
                {
                    BillingCycleAnchor = billingAnchorCycle,
                    TaxPercent = taxPercent,
                    PlanId = planId, 
                    CustomerId = stripeCustomerId
                });

            return stripeSubscription;
        }
       
    }
}
