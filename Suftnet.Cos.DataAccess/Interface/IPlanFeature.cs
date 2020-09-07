namespace Suftnet.Cos.DataAccess
{
    using System.Collections.Generic;

    public interface IPlanFeature : IRepository<PlanFeatureDto>
    {
        bool IsPlanFeature(int planId, int featureId);
    }
}

