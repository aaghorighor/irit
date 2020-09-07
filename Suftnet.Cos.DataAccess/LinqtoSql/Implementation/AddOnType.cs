namespace Suftnet.Cos.DataAccess
{
    using Suftnet.DataFactory.LinqToSql;
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

   public class AddOnType : IAddOnType
    {     
        public bool Delete(Guid id)
        {
            bool response = false;    
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.AddonTypes.FirstOrDefault(o => o.Id == id);

                if (objToDelete != null)
                {
                    context.AddonTypes.Remove(objToDelete);
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
                var objToDelete = context.AddonTypes.Where(o => o.Id == tenantId);

                if (objToDelete != null)
                {
                    context.AddonTypes.RemoveRange(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }
            return response;         
        }

        public Guid Insert(AddonTypeDto entity)
        {
            Guid id;
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.AddonType() { Id = entity.Id,  TenantId = entity.TenantId, Active = entity.Active, IndexNo = entity.IndexNo,  Name = entity.Name, CreatedBy = entity.CreatedBy, CreatedDt = entity.CreatedDT };
                context.AddonTypes.Add(obj);
                context.SaveChanges();
                id = obj.Id;
            }
            return id;
        }

        public bool Update(AddonTypeDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.AddonTypes.SingleOrDefault(o => o.Id == entity.Id);

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

        public AddonTypeDto Get(Guid id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.AddonTypes
                              where o.Id == id                        
                              select new AddonTypeDto { Active = o.Active, IndexNo = o.IndexNo, Id = o.Id, Name = o.Name }).FirstOrDefault();
                return result;
            }
        }
        

        public List<AddonTypeDto> GetAll(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.AddonTypes
                              where o.TenantId == tenantId
                              orderby o.IndexNo  ascending
                              select new AddonTypeDto { CreatedBy = o.CreatedBy, CreatedDT = o.CreatedDt, Active = o.Active, IndexNo = o.IndexNo, Id = o.Id, Name = o.Name }).ToList();
                return result;
            }
        }

        public List<MobileAddonTypeDto> Fetch(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var result = (from o in context.AddonTypes
                              where o.TenantId == tenantId && o.Active == true
                              orderby o.IndexNo ascending
                              select new MobileAddonTypeDto { Id = o.Id, Name = o.Name }).ToList();
                return result;
            }
        }

    }
}
