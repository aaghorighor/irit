namespace Suftnet.Cos.Web
{
    using global::Stripe;  
    using System.Collections.Generic;

    public class CardAdapter
    {
        public CardAdapter()
        {
            StripeCard = new List<Card>();
        }
        public List<Card> StripeCard { get; set; }
        public string StripeCustomerId { get; set; }
    }
}