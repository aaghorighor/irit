namespace Suftnet.Cos.Stripe
{
    using global::Stripe;
    using System.Collections.Generic;
    using System.Linq;

    public class InvoiceProvider : IInvoiceProvider
    {
        private readonly InvoiceService _stripeInvoiceService;
        public InvoiceProvider(string stripeProviderId)
        {
            this._stripeInvoiceService = new InvoiceService(stripeProviderId);
        }

        public List<CustomerInvoice> GetAll(string stripeCustomerId)
        {
            var invoices = new List<CustomerInvoice>();

            var stripeInvoices = this._stripeInvoiceService.List(new InvoiceListOptions { CustomerId = stripeCustomerId });

            foreach(var invoice in stripeInvoices)
            {
                invoices.Add(Mapper.Map(invoice));
            }

            return invoices;
        }

        public List<CustomerInvoice> GetCurrent(string stripeCustomerId)
        {
            var invoices = new List<CustomerInvoice>();

            var stripeInvoices = this._stripeInvoiceService.List(new InvoiceListOptions { CustomerId = stripeCustomerId });

            foreach (var invoice in stripeInvoices)
            {
                invoices.Add(Mapper.Map(invoice));
            }

            return invoices.OrderByDescending(x=>x.PeriodEnd).Take(1).ToList();
        }

        public CustomerInvoice Get(string stripeCustomerId, string invoiceId)
        {
            var invoices = new List<CustomerInvoice>();

            var stripeInvoices = this._stripeInvoiceService.List(new InvoiceListOptions { CustomerId = stripeCustomerId });

            foreach (var invoice in stripeInvoices)
            {
                invoices.Add(Mapper.Map(invoice));
            }
        
            return invoices.FirstOrDefault(x=>x.Id == invoiceId);
        }
    }
}
