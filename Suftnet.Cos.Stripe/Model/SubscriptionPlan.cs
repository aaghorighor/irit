namespace Suftnet.Cos.Stripe
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Suftnet.Cos.Common;
       
    public class SubscriptionPlan
    {       
        public SubscriptionPlan()
        {
            this.Properties = new List<SubscriptionPlanProperty>();
        }
       
        public string Id { get; set; }
        public int IntervalCount { get; set; }
        public string Name { get; set; }               
        public double Price { get; set; }
        public string Currency { get; set; }

        public CurrencyModel CurrencyDetails {
            get { return Suftnet.Cos.Common.Currency.GetCurrencyInfo(Currency); } 
        }
      
        public SubscriptionInterval Interval { get; set; }
              
        public int TrialPeriodInDays { get; set; }

        public bool Disabled { get; set; }
               
        public virtual ICollection<SubscriptionPlanProperty> Properties { get; set; }

        public string GetProperty(string key)
        {
            return this.Properties.Where(i => key != null && i.Key == key).Select(i => i.Value).FirstOrDefault();
        }
      
        public int GetPropertyInt(string key)
        {
            var property = this.Properties.Where(i => key != null && i.Key == key).Select(i => i.Value).FirstOrDefault();

            if (property != null)
            {
                return int.Parse(property);
            }
             
            throw new Exception("Property for key: " + key + "does not exist.");
        }
      
        public long GetPropertyLong(string key)
        {
            var property = this.Properties.Where(i => key != null && i.Key == key).Select(i => i.Value).FirstOrDefault();

            if (property != null)
            {
                return long.Parse(property);
            }

            throw new Exception("Property for key: " + key + "does not exist.");
        }
     
        public enum SubscriptionInterval
        {          
            Monthly = 1,
            Yearly = 2,
            Weekly = 3,
            EverySixMonths = 4,
            EveryThreeMonths = 5
        }
    }
}