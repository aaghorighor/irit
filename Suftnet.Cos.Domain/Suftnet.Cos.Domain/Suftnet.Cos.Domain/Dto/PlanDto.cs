namespace Suftnet.Cos.DataAccess
{  
    public class PlanDto : BaseDto
    {  
        public string Product { get; set; }
        public int ProductId { get; set; }
        public decimal? BasicPrice { get; set; }
        public decimal? AdvancePrice { get; set; }
        public decimal? ProfessionalPrice { get; set; }        
    }
}
