namespace Suftnet.Cos.Stripe
{
    using global::Stripe;
   
    public class ChargeProvider : IChargeProvider
    {       
        private readonly ChargeService _chargeService;
        private readonly CardService _cardService;
               
        public ChargeProvider(string stripeProviderId)
        {
            _chargeService = new ChargeService(stripeProviderId);
            _cardService = new CardService(stripeProviderId);
        }
      
        public bool Charge(long amount, string currency, string customerId, string sourceToken, string description, out string error)
        {                
            var card = _cardService.Create(customerId, new CardCreateOptions { SourceToken = sourceToken });

            if(card != null)
            {
                var options = new ChargeCreateOptions
                {
                    Amount = (long)(amount * 100),
                    Currency = currency,                  
                    SourceId = card.Id,
                    CustomerId = customerId,
                    Description = description
                };

                var result = _chargeService.Create(options);

                if (result.Captured != null && result.Captured.Value)
                {
                    error = null;
                    return true;
                }
                else
                {
                    error = result.FailureMessage;
                    return false;
                }
            }

            error = "Customer cannot be created";
            return false;
        }
    }
}
