namespace Suftnet.Cos.DataAccess
{ 
    public class PlanFeatureDto : BaseDto
    {
        public int ProductFeatureId { get; set; }
        public string Feature { get; set; }
        public int BasicId { get; set; }
        public string Basic { get; set; }
        public int AdvanceId { get; set; }
        public string Advance { get; set; }
        public int ProfessionalId { get; set; }
        public int PlanId { get; set; }       
        public string Professional { get; set; }
        public string BasicValue { get; set; }
        public string PremiumValue { get; set; }
        public string PremiumPlusValue { get; set; }
        public int? IndexNo { get; set; }
    }
}
