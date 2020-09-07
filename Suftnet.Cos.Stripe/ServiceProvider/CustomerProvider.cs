namespace Suftnet.Cos.Stripe
{
    using global::Stripe;
    using System.Collections.Generic;

    public class CustomerProvider : ICustomerProvider
    {
        private readonly CustomerService _customerService;

        public CustomerProvider(string stripeProviderId)
        {
            _customerService = new CustomerService(stripeProviderId);
        }

        public string Create(string email)
        {
            var customer = new CustomerCreateOptions
            {
                Email = email
            };

            var stripeUser = _customerService.Create(customer);
            return stripeUser.Id;
        }
        public string Create(string email, string StripeToken, string planType, decimal? taxRate, Dictionary<string, string> metadata)
        {
            var customer = new CustomerCreateOptions()
            {
                Email = email,
                SourceToken = StripeToken,
                PlanId = planType,
                TaxPercent = taxRate,
                Metadata = metadata
            };

            var stripeCustomer = _customerService.Create(customer);

            return stripeCustomer.Id;
        }

        public string Create(string email, string StripeToken)
        {
            var customer = new CustomerCreateOptions()
            {
                Email = email,
                SourceToken = StripeToken
            };

            var stripeCustomer = _customerService.Create(customer);

            return stripeCustomer.Id;
        }
        public object Update(string email, string stripeCustomerId)
        {
            var customer = new CustomerUpdateOptions
            {
                Email = email
            };

            return _customerService.Update(stripeCustomerId, customer);
        }
        public string GetCustomerId(string stripeCustomerId)
        {
            var stripeCustomer = _customerService.Get(stripeCustomerId);
            return stripeCustomer.Id;
        }

        public Customer GetCustomer(string stripeCustomerId)
        {
            var stripeCustomer = _customerService.Get(stripeCustomerId);
            return stripeCustomer;
        }
        public void Delete(string stripeCustomerId)
        {
            _customerService.Delete(stripeCustomerId);
        }

    }
}
