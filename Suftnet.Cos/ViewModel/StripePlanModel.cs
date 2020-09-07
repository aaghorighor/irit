namespace Suftnet.Cos.Web.ViewModel
{
    public class StripePlanModel
    {
        public string BillingCycle { get; set; }
        public string Plan { get; set; }
        public int PlanId { get; set; }
        public string PlanTypeId { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Vat { get; set; }
        public decimal? Total { get; set; }
        public string PrivacyPolicy { get; set; }
        public string TermsOfUsed { get; set; }

    }
}