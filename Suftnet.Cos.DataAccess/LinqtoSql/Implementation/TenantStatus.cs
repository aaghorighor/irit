namespace Suftnet.Cos.DataAccess
{
    using Suftnet.DataFactory.LinqToSql;
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

   public class TenantStatus : ITenantStatus
    {     
        public bool Delete(Guid id)
        {
            bool response = false;    
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.TenantStates.FirstOrDefault(o => o.Id == id);

                if (objToDelete != null)
                {
                    context.TenantStates.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }
            return response;           
        }       
        public Guid Insert(CommonTypeDto entity)
        {
            Guid id;
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.TenantState() { Id = entity.Id,  Active = entity.Active, IndexNo = entity.IndexNo,  Name = entity.Name, CreatedBy = entity.CreatedBy, CreatedDt = entity.CreatedDT };
                context.TenantStates.Add(obj);
                context.SaveChanges();
                id = obj.Id;
            }
            return id;
        }
        public bool Update(CommonTypeDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.TenantStates.SingleOrDefault(o => o.Id == entity.Id);

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
        public CommonTypeDto Get(Guid id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.TenantStates
                              where o.Id == id                        
                              select new CommonTypeDto { Active = o.Active, IndexNo = o.IndexNo, Id = o.Id, Name = o.Name }).FirstOrDefault();
                return result;
            }
        }        
        public List<CommonTypeDto> GetAll()
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.TenantStates
                              orderby o.IndexNo  ascending
                              select new CommonTypeDto { CreatedBy = o.CreatedBy, CreatedDT = o.CreatedDt, Active = o.Active, IndexNo = o.IndexNo, Id = o.Id, Name = o.Name }).ToList();
                return result;
            }
        }
        public List<MobileCommonTypeDto> Fetch()
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.TenantStates
                              where o.Active == true
                              orderby o.IndexNo ascending
                              select new MobileCommonTypeDto { Id = o.Id, Name = o.Name }).ToList();
                return result;
            }
        }

    }
}
