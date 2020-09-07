namespace Suftnet.Cos.DataAccess
{
    using Suftnet.DataFactory.LinqToSql;
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

   public class Tax : ITax
    {     
        public bool Delete(Guid id)
        {
            bool response = false;    
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.Taxes.FirstOrDefault(o => o.Id == id);

                if (objToDelete != null)
                {
                    context.Taxes.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }
            return response;           
        }

        public bool DeleteTenant(Guid tenantId)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.Taxes.Where(o => o.Id == tenantId);

                if (objToDelete != null)
                {
                    context.Taxes.RemoveRange(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }
            return response;         
        }

        public Guid Insert(TaxDto entity)
        {
            Guid id;
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.Tax() { Rate = entity.Rate, Id = entity.Id, TenantId = entity.TenantId, Active = entity.Active, IndexNo = entity.IndexNo,  Name = entity.Name, CreatedBy = entity.CreatedBy, CreatedDt = entity.CreatedDT };
                context.Taxes.Add(obj);
                context.SaveChanges();
                id = obj.Id;
            }
            return id;
        }

        public bool Update(TaxDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Taxes.SingleOrDefault(o => o.Id == entity.Id);

                if (objToUpdate != null)
                {
                    objToUpdate.Active = entity.Active;
                    objToUpdate.Name = entity.Name;
                    objToUpdate.IndexNo = entity.IndexNo;                   
                    objToUpdate.Rate = entity.Rate;

                    try
                    {
                        context.SaveChanges();
                        response = true;
                    }
                    catch (ChangeConflictException)
                    {                   
                        response = true;
                    }
                }

            }
            return response;
        }

        public TaxDto Get(Guid id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.Taxes
                              where o.Id == id                        
                              select new TaxDto { Active = o.Active, IndexNo = o.IndexNo, Rate = o.Rate, Id = o.Id, Name = o.Name }).FirstOrDefault();
                return result;
            }
        }
        

        public List<TaxDto> GetAll(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.Taxes
                              where o.TenantId == tenantId
                              orderby o.IndexNo  ascending
                              select new TaxDto { CreatedBy = o.CreatedBy, CreatedDT = o.CreatedDt, Active = o.Active, IndexNo = o.IndexNo, Rate = o.Rate, Id = o.Id, Name = o.Name }).ToList();
                return result;
            }
        }

        public List<MobileTaxDto> Fetch(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.Taxes
                              where o.TenantId == tenantId && o.Active == true
                              orderby o.IndexNo ascending
                              select new MobileTaxDto { Id = o.Id, Name = o.Name }).ToList();
                return result;
            }
        }

    }
}
