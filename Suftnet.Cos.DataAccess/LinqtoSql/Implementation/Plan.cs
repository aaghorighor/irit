namespace Suftnet.Cos.DataAccess
{
    using System.Collections.Generic;
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
    using System.Data.Linq;

    public class Plan : IPlan
    {
        public PlanDto Get(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Plans                                                                                
                                 where o.Id == Id
                                 select new PlanDto { ProductId = o.ProductId, AdvancePrice = o.AdvancePrice, BasicPrice = o.BasicPrice, ProfessionalPrice = o.ProfessionalPrice, CreatedDT= o.CreatedDt, CreatedBy = o.CreatedBy, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }

        public bool IsProductInPlan(int productId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Plans
                                 where o.ProductId == productId
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
                var objToDelete = context.Plans.SingleOrDefault(o => o.Id == Id);
                if (objToDelete != null)
                {
                    context.Plans.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }

            return response;
        }

        public int Insert(PlanDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.Plan() { AdvancePrice = entity.AdvancePrice, ProfessionalPrice = entity.ProfessionalPrice, BasicPrice = entity.BasicPrice, ProductId = entity.ProductId, CreatedBy = entity.CreatedBy, CreatedDt = entity.CreatedDT };
                context.Plans.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }

        public bool Update(PlanDto entity)
        {
            bool response = false;

            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Plans.SingleOrDefault(o => o.Id == entity.Id);

                if (objToUpdate != null)
                {
                    objToUpdate.AdvancePrice = entity.AdvancePrice;
                    objToUpdate.BasicPrice = entity.BasicPrice;              
                    objToUpdate.ProfessionalPrice = entity.ProfessionalPrice;                               
                    
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

        public List<PlanDto> GetAll(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Plans                               
                                 where o.ProductId == Id
                                 select new PlanDto { ProductId = o.ProductId, AdvancePrice = o.AdvancePrice, BasicPrice = o.BasicPrice, ProfessionalPrice = o.ProfessionalPrice, CreatedDT= o.CreatedDt, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;           
            }
        }

        public List<PlanDto> GetAll()
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Plans                             
                                 orderby o.CreatedDt descending
                                 select new PlanDto { ProductId = o.ProductId, AdvancePrice = o.AdvancePrice, BasicPrice = o.BasicPrice, ProfessionalPrice = o.ProfessionalPrice, CreatedDT= o.CreatedDt, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }     
        }

        public PlanFeatureAdapter GetPlanFeatures(int productId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Plans                                
                                 let planFeature = (from a in context.PlanFeatures
                                                    join f in context.Commons on a.ProductFeatureId equals f.ID
                                                    join d in context.Commons on a.AdvanceId equals d.ID
                                                    join b in context.Commons on a.BasicId equals b.ID
                                                    join p in context.Commons on a.ProfessionId equals p.ID
                                                    where a.PlanId == o.Id
                                                    orderby a.IndexNo
                                                    select new PlanFeatureDto {  BasicValue = a.BasicValue, PremiumValue = a.PremiumValue, PremiumPlusValue = a.PremiumPlusValue, Feature = f.Title, AdvanceId = a.AdvanceId, Advance = d.Title, BasicId = a.BasicId, Basic = b.Title, ProfessionalId = a.ProfessionId, Professional = p.Title, CreatedDT = o.CreatedDt, CreatedBy = a.CreatedBy, Id = a.Id }).ToList()
                                 where o.ProductId == productId
                                 orderby o.CreatedDt descending
                                 select new PlanFeatureAdapter { PlanFeature = planFeature, Plan = new PlanDto { ProductId = o.ProductId, AdvancePrice = o.AdvancePrice, BasicPrice = o.BasicPrice, ProfessionalPrice = o.ProfessionalPrice, Id = o.Id } }).FirstOrDefault();
                return objResult != null ? objResult : new PlanFeatureAdapter();
            }
        }    
    }
}


