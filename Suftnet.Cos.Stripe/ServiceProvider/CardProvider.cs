namespace Suftnet.Cos.Stripe
{
    using System;
    using System.Collections.Generic;
    using global::Stripe;

    public class CardProvider : ICardProvider
    {      
        private readonly CardService _cardService;   
        public CardProvider(string stripeProviderId)
        {           
            this._cardService = new CardService(stripeProviderId);
        }     
       
        public void Delete(string stripeCustomerId, string cardId)
        {          
            this._cardService.Delete(stripeCustomerId, cardId);          
        }

        public List<Card> GetAll(string stripeCustomerId)
        {
            var stripeCards = new List<Card>();

            var lstCard = this._cardService.List(stripeCustomerId);

            foreach (var card in lstCard)
            {
                stripeCards.Add(card);
            }

            return stripeCards;
        }
       
        public Card Create(string sourceToken, string stripeCustomerId)
        {
            var options = new CardCreateOptions
            {
                 SourceToken = sourceToken
            };

            return _cardService.Create(stripeCustomerId, options);
        }        
    }
}
