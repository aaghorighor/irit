namespace Suftnet.Cos.Stripe
{
    using System;
    using System.Collections.Generic;

    public class CustomerInvoice
    {
        public string Id { get; set; }
        public string StripeId { get; set; }
        public string StripeCustomerId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? PeriodStart { get; set; }
        public DateTime? PeriodEnd { get; set; }
        public string Currency { get; set; }
        public bool? Forgiven { get; set; }
        public string StatementDescriptor { get; set; }
        public string ReceiptNumber { get; set; }
        public string Number { get; set; }
        public string Description {get;set;}
        public long? Subtotal { get; set; }
        public long? Total { get; set; }
        public bool? Attempted { get; set; }
        public bool? Closed { get; set; }
        public bool? Paid { get; set; }
        public long? AttemptCount { get; set; }
        public long? AmountDue { get; set; }
        public long? StartingBalance { get; set; }
        public long? EndingBalance { get; set; }
        public DateTime? NextPaymentAttempt { get; set; }
        public int? Charge { get; set; }
        public long? Discount { get; set; }
        public long? ApplicationFee { get; set; }
        public string CurrencySymbol { get; set; }
        public string InvoicePeriod { get; set; }
        public long? Tax { get; set; }
        public string Status { get; set; }     
        public string Hosted_invoice_url { get; set; }
        public string Invoice_pdf { get; set; }
        public decimal? TaxPercent { get; set; }
        public DateTime? DueDate { get; set; }
        public ICollection<LineItem> LineItems { get; set; }       
    }

    public class LineItem
    {
        public int Id { get; set; }
        public string StripeLineItemId { get; set; }
        public string Type { get; set; }
        public int? Amount { get; set; }
        public string Currency { get; set; }
        public bool Proration { get; set; }
        public CustomerPeriod Period { get; set; }
        public int? Quantity { get; set; }
        public CustomerPlan Plan { get; set; }
    }
    public class CustomerPlan
    {
        public string StripePlanId { get; set; }
        public string Interval { get; set; }
        public string Name { get; set; }
        public DateTime? Created { get; set; }
        public int? AmountInCents { get; set; }
        public string Currency { get; set; }
        public int IntervalCount { get; set; }
        public int? TrialPeriodDays { get; set; }
        public string StatementDescription { get; set; }
    }

    public class CustomerPeriod
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
