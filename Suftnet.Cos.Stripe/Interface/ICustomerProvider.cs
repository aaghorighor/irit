namespace Suftnet.Cos.Stripe
{
    using global::Stripe;
    using System.Collections.Generic;
    public interface ICustomerProvider
    {
        string GetCustomerId(string stripeCustomerId);
        Customer GetCustomer(string stripeCustomerId);
        string Create(string email);
        string Create(string email, string StripeToken);
        string Create(string email, string StripeToken, string planType, decimal? taxRate, Dictionary<string, string> metadata);
        object Update(string email, string stripeCustomerId);
        void Delete(string stripeCustomerId);     
    }
}
