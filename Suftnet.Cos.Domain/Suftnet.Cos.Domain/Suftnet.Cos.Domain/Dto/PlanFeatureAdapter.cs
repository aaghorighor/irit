namespace Suftnet.Cos.DataAccess
{   
    using System.Collections.Generic;

    public class PlanFeatureAdapter : BaseDto
    {
        public PlanFeatureAdapter()
        {           
            PlanFeature = new List<PlanFeatureDto>();
        }

        public TenantDto Tenant { get; set; }
        public PlanDto Plan { get; set; }
        public List<PlanFeatureDto> PlanFeature { get; set; }       
    }
}
