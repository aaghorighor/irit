namespace Suftnet.Cos.Stripe
{
    using System;

    using global::Stripe;

    public interface ISubscriptionProvider
    {       
        string SubscriptionId(string stripeCustomerId);
        Subscription Get(string stripeCustomerId);
        Subscription GetSubscriptionByCustomerId(string stripeCustomerId);
        DateTime CancelSubscription(string stripeCustomerId, bool cancelAtPeriodEnd = false);
        Subscription UpdateSubscription(string stripeCustomerId, string newPlanId, bool proRate);
        void UpdateSubscription(string stripeCustomerId, decimal taxPercent = 0);
        Subscription SubscribeUserNaturalMonth(string stripeCustomerId, string planId, DateTime? billingAnchorCycle, decimal? taxPercent);        
    }
}
