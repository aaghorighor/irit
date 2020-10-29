namespace Suftnet.Cos.DataAccess
{  
    public interface IPlan : IRepository<PlanDto>
    {
        PlanFeatureAdapter GetPlanFeatures();
        bool IsProductInPlan(int productId);
    }
}

