namespace Suftnet.Cos.Stripe
{
    using global::Stripe;
    using System.Collections.Generic;
    using System.Linq;

    public static  class Mapper
    {
        public static CustomerInvoice Map(Invoice stripeInvoice)
        {
            var invoice = new CustomerInvoice
            {
                AmountDue = stripeInvoice.AmountDue,
                ApplicationFee = stripeInvoice?.ApplicationFeeAmount,
                AttemptCount = stripeInvoice.AttemptCount,
                Attempted = stripeInvoice.Attempted,
                Status = stripeInvoice.Status,
                Currency = stripeInvoice.Currency,
                Date = stripeInvoice.Created,
                DueDate = stripeInvoice.DueDate,
                Description = stripeInvoice?.Description,
                // Discount = Map(stripeInvoice.StripeDiscount),
                EndingBalance = stripeInvoice?.EndingBalance,               
                NextPaymentAttempt = stripeInvoice?.NextPaymentAttempt,
                Paid = stripeInvoice.Paid,
                PeriodStart = stripeInvoice.PeriodStart,
                PeriodEnd = stripeInvoice.PeriodEnd,
                ReceiptNumber = stripeInvoice?.ReceiptNumber,
                StartingBalance = stripeInvoice.StartingBalance,
                StripeCustomerId = stripeInvoice.CustomerId,
                StatementDescriptor = stripeInvoice?.StatementDescriptor,
                Tax = stripeInvoice.Tax,
                Number = stripeInvoice.Number,
                TaxPercent = stripeInvoice.TaxPercent,
                Id = stripeInvoice.Id,
                Subtotal = stripeInvoice.Subtotal,
                Total = stripeInvoice.Total,
                Hosted_invoice_url = stripeInvoice.HostedInvoiceUrl,
                Invoice_pdf = stripeInvoice.InvoicePdf,
                   
                LineItems = Map(stripeInvoice.Lines)
            };

            return invoice;
        }

        private static ICollection<LineItem> Map(IEnumerable<InvoiceLineItem> list)
        {
            if (list == null)
                return null;

            return list.Select(i => new LineItem
            {
                Amount =(int)i.Amount,
                Currency = i.Currency,
                Period = Map(i.Period),
                Plan = Map(i.Plan),
                Proration = i.Proration,
                Quantity =(int)i.Quantity,
                StripeLineItemId = i.Id,
                Type = i.Type
            }).ToList();
        }

        private static CustomerPlan Map(Plan stripePlan)
        {
            return new CustomerPlan
            {
                AmountInCents = (int)stripePlan.Amount,
                Created = stripePlan.Created,
                Currency = stripePlan.Currency,            
                Interval = stripePlan.Interval,
                IntervalCount = (int)stripePlan.IntervalCount,
                Name = stripePlan.Nickname,
                StripePlanId = stripePlan.Id,
                TrialPeriodDays = stripePlan.TrialPeriodDays != null ? (int)stripePlan.TrialPeriodDays : 0
            };
        }

        private static CustomerPeriod Map(Period stripePeriod)
        {
            if (stripePeriod == null)
                return null;

            return new CustomerPeriod
            {
                Start = stripePeriod.Start,
                End = stripePeriod.End
            };
        }
    }
}
