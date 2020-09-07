namespace Suftnet.Cos.DataAccess
{
    using Suftnet.DataFactory.LinqToSql;
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

   public class Unit : IUnit
    {     
        public bool Delete(Guid id)
        {
            bool response = false;    
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.Units.FirstOrDefault(o => o.Id == id);

                if (objToDelete != null)
                {
                    context.Units.Remove(objToDelete);
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
                var objToDelete = context.Units.Where(o => o.Id == tenantId);

                if (objToDelete != null)
                {
                    context.Units.RemoveRange(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }
            return response;         
        }

        public Guid Insert(UnitDto entity)
        {
            Guid id;
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.Unit() { Id = entity.Id, TenantId = entity.TenantId, Active = entity.Active, IndexNo = entity.IndexNo,  Name = entity.Name, CreatedBy = entity.CreatedBy, CreatedDt = entity.CreatedDT };
                context.Units.Add(obj);
                context.SaveChanges();
                id = obj.Id;
            }
            return id;
        }

        public bool Update(UnitDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Units.SingleOrDefault(o => o.Id == entity.Id);

                if (objToUpdate != null)
                {
                    objToUpdate.Active = entity.Active;
                    objToUpdate.Name = entity.Name;
                    objToUpdate.IndexNo = entity.IndexNo;                   
                  
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

        public UnitDto Get(Guid id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.Units
                              where o.Id == id                        
                              select new UnitDto { Active = o.Active, IndexNo = o.IndexNo, Id = o.Id, Name = o.Name }).FirstOrDefault();
                return result;
            }
        }
        

        public List<UnitDto> GetAll(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.Units
                              where o.TenantId == tenantId
                              orderby o.IndexNo  ascending
                              select new UnitDto { CreatedBy = o.CreatedBy, CreatedDT = o.CreatedDt, Active = o.Active, IndexNo = o.IndexNo, Id = o.Id, Name = o.Name }).ToList();
                return result;
            }
        }

        public List<MobileUnitDto> Fetch(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.Units
                              where o.TenantId == tenantId && o.Active == true
                              orderby o.IndexNo ascending
                              select new MobileUnitDto { Id = o.Id, Name = o.Name }).ToList();
                return result;
            }
        }

    }
}
