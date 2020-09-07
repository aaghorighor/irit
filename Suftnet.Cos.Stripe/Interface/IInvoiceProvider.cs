namespace Suftnet.Cos.Stripe
{
    using global::Stripe;
    using System.Collections.Generic;

    public interface IInvoiceProvider
    {
        List<CustomerInvoice> GetAll(string stripeCustomerId);
        List<CustomerInvoice> GetCurrent(string stripeCustomerId);
        CustomerInvoice Get(string stripeCustomerId, string invoiceId);
    }
}
