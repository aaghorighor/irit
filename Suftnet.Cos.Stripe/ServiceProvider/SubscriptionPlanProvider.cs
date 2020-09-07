namespace Suftnet.Cos.Stripe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;  

    using global::Stripe;
      
    public class SubscriptionPlanProvider : ISubscriptionPlanProvider
    {      
        private readonly PlanService _planService;       
        
        public SubscriptionPlanProvider(string stripeProviderId)
        {
            _planService = new PlanService(stripeProviderId);
        }
        
        public object Add(SubscriptionPlan stripePlan)
        {
            var result = _planService.Create(new PlanCreateOptions
            {
                Id = stripePlan.Id,
                Nickname = stripePlan.Name,
                Amount = (int)Math.Round(stripePlan.Price * 100),
                Currency = stripePlan.Currency,
                Interval = GetInterval(stripePlan.Interval),
                TrialPeriodDays = stripePlan.TrialPeriodInDays,
                IntervalCount = stripePlan.IntervalCount
            });

            return result;
        }

        public object Update(SubscriptionPlan plan)
        {
            var res = _planService.Update(plan.Id, new PlanUpdateOptions
            {
               Nickname = plan.Name
            });

            return res;
        }
       
        public void Delete(string planId)
        {
            _planService.Delete(planId);
        }
      
        public SubscriptionPlan Find(string planId)
        {
            var stripePlan = _planService.Get(planId);
            return SubscriptionPlanMapper(stripePlan);
        }
       
        public IEnumerable<SubscriptionPlan> GetAllAsync(object options)
        {
            var result = _planService.List();
            return result.Select(SubscriptionPlanMapper);
        }
        
        private static string GetInterval(SubscriptionPlan.SubscriptionInterval interval)
        {
            string result = null;

            switch (interval)
            {
                case (SubscriptionPlan.SubscriptionInterval.Monthly):
                    result = "month";
                    break;
                case (SubscriptionPlan.SubscriptionInterval.Yearly):
                    result = "year";
                    break;
                case (SubscriptionPlan.SubscriptionInterval.Weekly):
                    result = "week";
                    break;
                case (SubscriptionPlan.SubscriptionInterval.EveryThreeMonths):
                    result = "3-month";
                    break;
                case (SubscriptionPlan.SubscriptionInterval.EverySixMonths):
                    result = "6-month";
                    break;
            }

            return result;
        }

        private static SubscriptionPlan.SubscriptionInterval GetInterval(string interval)
        {
            switch (interval)
            {
                case ("month"):
                    return SubscriptionPlan.SubscriptionInterval.Monthly;
                case ("year"):
                    return SubscriptionPlan.SubscriptionInterval.Yearly;
                case ("week"):
                    return SubscriptionPlan.SubscriptionInterval.Weekly;
                case ("3-month"):
                    return SubscriptionPlan.SubscriptionInterval.EveryThreeMonths;
                case ("6-month"):
                    return SubscriptionPlan.SubscriptionInterval.EverySixMonths;
            }

            return 0;
        }

        private static SubscriptionPlan SubscriptionPlanMapper(Plan stripePlan)
        {
            return new SubscriptionPlan
            {
                Id = stripePlan.Id,
                Name = stripePlan.Nickname,
                Currency = stripePlan.Currency,
                Interval = GetInterval(stripePlan.Interval),
                Price = Math.Round((double)stripePlan.Amount * 100),
                TrialPeriodInDays = stripePlan.TrialPeriodDays == null ? (int)stripePlan.TrialPeriodDays : 0
            };
        }
    }
}
