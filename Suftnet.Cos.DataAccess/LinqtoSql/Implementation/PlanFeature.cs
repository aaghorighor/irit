namespace Suftnet.Cos.DataAccess
{
    using System.Collections.Generic;
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
    using System.Data.Linq;

    public class PlanFeature : IPlanFeature
    {
        public PlanFeatureDto Get(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.PlanFeatures                                
                                 join s in context.Commons on o.FeatureId equals s.ID
                                 join a in context.Commons on o.AdvanceId equals a.ID
                                 join b in context.Commons on o.BasicId equals b.ID
                                 join p in context.Commons on o.ProfessionId equals p.ID   
                                 where o.Id == Id
                                 select new PlanFeatureDto { IndexNo = o.IndexNo, BasicValue = o.BasicValue, PremiumValue = o.PremiumValue, PremiumPlusValue = o.PremiumPlusValue, FeatureId = o.FeatureId, PlanId = o.PlanId, Feature = s.Title, AdvanceId = o.AdvanceId,  Advance =a.Title, BasicId =o.BasicId, Basic = b.Title, ProfessionalId = o.ProfessionId, Professional = p.Title, CreatedDT= o.CreatedDt, CreatedBy = o.CreatedBy, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }

        public bool IsPlanFeature(int planId, int featureId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.PlanFeatures
                                 where o.PlanId == planId && o.FeatureId == featureId
                                 select o).FirstOrDefault();

                if (objResult != null)
                {
                    return true;
                }

                return false;
            }
        }

        public bool Delete(int Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.PlanFeatures.SingleOrDefault(o => o.Id == Id);
                if (objToDelete != null)
                {
                    context.PlanFeatures.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }

            return response;
        }

        public int Insert(PlanFeatureDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.PlanFeature() { IndexNo = entity.IndexNo, BasicValue = entity.BasicValue, PremiumValue = entity.PremiumValue, PremiumPlusValue = entity.PremiumPlusValue, PlanId = entity.PlanId, FeatureId = entity.FeatureId, ProfessionId = entity.ProfessionalId, AdvanceId = entity.AdvanceId, BasicId = entity.BasicId, CreatedBy = entity.CreatedBy, CreatedDt = entity.CreatedDT };
                context.PlanFeatures.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }

        public bool Update(PlanFeatureDto entity)
        {
            bool response = false;

            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.PlanFeatures.SingleOrDefault(o => o.Id == entity.Id);

                if (objToUpdate != null)
                {
                    objToUpdate.PlanId = entity.PlanId;
                    objToUpdate.FeatureId = entity.FeatureId;
                    objToUpdate.ProfessionId = entity.ProfessionalId;
                    objToUpdate.AdvanceId = entity.AdvanceId;
                    objToUpdate.BasicValue = entity.BasicValue;
                    objToUpdate.PremiumValue = entity.PremiumValue;
                    objToUpdate.PremiumPlusValue = entity.PremiumPlusValue;
                    objToUpdate.BasicId = entity.BasicId;
                    objToUpdate.IndexNo = entity.IndexNo;

                    try
                    {
                        context.SaveChanges();
                        response = true;
                    }
                    catch (ChangeConflictException)
                    {
                   
                        context.SaveChanges();
                        response = true;
                    }
                }

                return response;
            }
        }

        public List<PlanFeatureDto> GetAll(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.PlanFeatures
                                 join s in context.Commons on o.FeatureId equals s.ID
                                 join a in context.Commons on o.AdvanceId equals a.ID
                                 join b in context.Commons on o.BasicId equals b.ID
                                 join p in context.Commons on o.ProfessionId equals p.ID
                                 where o.PlanId == Id
                                 orderby o.IndexNo  
                                 select new PlanFeatureDto { IndexNo = o.IndexNo, BasicValue = o.BasicValue, PremiumValue = o.PremiumValue, PremiumPlusValue = o.PremiumPlusValue, Feature = s.Title, AdvanceId = o.AdvanceId, Advance = a.Title, BasicId = o.BasicId, Basic = b.Title, ProfessionalId = o.ProfessionId, Professional = p.Title, CreatedDT= o.CreatedDt, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;          
            }
        }

        public List<PlanFeatureDto> GetAll()
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.PlanFeatures
                                 join s in context.Commons on o.FeatureId equals s.ID
                                 join a in context.Commons on o.AdvanceId equals a.ID
                                 join b in context.Commons on o.BasicId equals b.ID
                                 join p in context.Commons on o.ProfessionId equals p.ID
                                 orderby o.IndexNo 
                                 select new PlanFeatureDto { IndexNo = o.IndexNo, BasicValue = o.BasicValue, PremiumValue = o.PremiumValue, PremiumPlusValue = o.PremiumPlusValue, Feature = s.Title, AdvanceId = o.AdvanceId, Advance = a.Title, BasicId = o.BasicId, Basic = b.Title, ProfessionalId = o.ProfessionId, Professional = p.Title, CreatedDT= o.CreatedDt, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }  
        }         
    }
}


