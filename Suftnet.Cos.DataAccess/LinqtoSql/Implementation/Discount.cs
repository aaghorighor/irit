namespace Suftnet.Cos.DataAccess
{
    using Suftnet.DataFactory.LinqToSql;
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

   public class Discount : IDiscount
    {     
        public bool Delete(Guid id)
        {
            bool response = false;    
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.Discounts.FirstOrDefault(o => o.Id == id);

                if (objToDelete != null)
                {
                    context.Discounts.Remove(objToDelete);
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
                var objToDelete = context.Discounts.Where(o => o.Id == tenantId);

                if (objToDelete != null)
                {
                    context.Discounts.RemoveRange(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }
            return response;         
        }

        public Guid Insert(DiscountDto entity)
        {
            Guid id;
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.Discount() { Rate = entity.Rate, Id = entity.Id, TenantId = entity.TenantId, Active = entity.Active, IndexNo = entity.IndexNo,  Name = entity.Name, CreatedBy = entity.CreatedBy, CreatedDt = entity.CreatedDT };
                context.Discounts.Add(obj);
                context.SaveChanges();
                id = obj.Id;
            }
            return id;
        }

        public bool Update(DiscountDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Discounts.SingleOrDefault(o => o.Id == entity.Id);

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

        public DiscountDto Get(Guid id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.Discounts
                              where o.Id == id                        
                              select new DiscountDto { Active = o.Active, IndexNo = o.IndexNo, Rate = o.Rate, Id = o.Id, Name = o.Name }).FirstOrDefault();
                return result;
            }
        }
        

        public List<DiscountDto> GetAll(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.Discounts
                              where o.TenantId == tenantId
                              orderby o.IndexNo  ascending
                              select new DiscountDto { CreatedBy = o.CreatedBy, CreatedDT = o.CreatedDt, Active = o.Active, IndexNo = o.IndexNo, Rate = o.Rate, Id = o.Id, Name = o.Name }).ToList();
                return result;
            }
        }

        public List<MobileDiscountDto> Fetch(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.Discounts
                              where o.TenantId == tenantId && o.Active == true
                              orderby o.IndexNo ascending
                              select new MobileDiscountDto { Id = o.Id, Name = o.Name }).ToList();
                return result;
            }
        }

    }
}
