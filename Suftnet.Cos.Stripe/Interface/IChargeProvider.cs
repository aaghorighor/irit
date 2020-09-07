namespace Suftnet.Cos.Stripe
{   
    public interface IChargeProvider
    {        
        bool Charge(long amount, string currency, string description, string customerId, string sourceToken, out string error);   
    }
}
