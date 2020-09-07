namespace Suftnet.Cos.Stripe
{
   using StructureMap.Configuration.DSL;  

   public class StripeRegistry : Registry
   {
        public StripeRegistry(string stripeProviderId)
       {
            For<IChargeProvider>().Use<ChargeProvider>().Ctor<string>("stripeProviderId").Is(stripeProviderId);
            For<ICustomerProvider>().Use<CustomerProvider>().Ctor<string>("stripeProviderId").Is(stripeProviderId);
            For<ICardProvider>().Use<CardProvider>().Ctor<string>("stripeProviderId").Is(stripeProviderId);
            For<ISubscriptionPlanProvider>().Use<SubscriptionPlanProvider>().Ctor<string>("stripeProviderId").Is(stripeProviderId);
            For<ISubscriptionProvider>().Use<SubscriptionProvider>().Ctor<string>("stripeProviderId").Is(stripeProviderId);
            For<IInvoiceProvider>().Use<InvoiceProvider>().Ctor<string>("stripeProviderId").Is(stripeProviderId);
        }
   } 
}
