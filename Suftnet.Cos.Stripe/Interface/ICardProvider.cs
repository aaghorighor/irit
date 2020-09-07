namespace Suftnet.Cos.Stripe
{
    using System.Collections.Generic;  
    using global::Stripe;
   
    public interface ICardProvider
    {
        Card Create(string sourceToken, string stripeCustomerId);
        void Delete(string stripeCustomerId, string cardId);
        List<Card> GetAll(string stripeCustomerId);
    }
}
