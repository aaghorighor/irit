namespace Suftnet.Cos.DataAccess
{  
    public interface IPlan : IRepository<PlanDto>
    {
        PlanFeatureAdapter GetPlanFeatures(int productId);
        bool IsProductInPlan(int productId);
    }
}

