namespace Suftnet.Cos.Stripe
{
    using System.Collections.Generic;
    
    public interface ISubscriptionPlanProvider
    {       
        object Add(SubscriptionPlan plan);        
        object Update(SubscriptionPlan plan);      
        void Delete(string planId);      
        SubscriptionPlan Find(string planId);      
        IEnumerable<SubscriptionPlan> GetAllAsync(object options);
    }
}
