namespace Suftnet.Cos.DataAccess
{
    using Suftnet.DataFactory.LinqToSql;
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

    public class Addon : IAddon
    {
        public AddonDto Get(Guid Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Addons
                                 join c in context.AddonTypes on o.AddonTypeId equals c.Id
                                 where o.Id == Id
                                 select new AddonDto { AddonType = c.Name, AddonTypeId = o.AddonTypeId, Active = o.Active, Price = o.Price, MenuId = o.MenuId, Name = o.Name, CreatedDT = o.CreatedDt, CreatedBy = o.CreatedBy, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }

        public bool Delete(Guid Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {

                var objToDelete = context.Addons.SingleOrDefault(o => o.Id.Equals(Id));
                if (objToDelete != null)
                {
                    context.Addons.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }

            return response;
        }

        public Guid Insert(AddonDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.Addon() { Id = entity.Id, AddonTypeId = entity.AddonTypeId, Active = entity.Active, Price = entity.Price, MenuId = entity.MenuId, Name = entity.Name, CreatedDt = entity.CreatedDT, CreatedBy = entity.CreatedBy };
                context.Addons.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }

        public bool Update(AddonDto entity)
        {
            bool response = false;

            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Addons.SingleOrDefault(o => o.Id == entity.Id);

                if (objToUpdate != null)
                {
                    objToUpdate.Active = entity.Active;                                   
                    objToUpdate.Price = entity.Price;
                    objToUpdate.MenuId = entity.MenuId;
                    objToUpdate.AddonTypeId = entity.AddonTypeId;
                    objToUpdate.Name = entity.Name;

                    try
                    {
                        context.SaveChanges();
                        response = true;
                    }
                    catch (ChangeConflictException)
                    {
                        response = false;
                    }
                }

                return response;
            }
        }        

        public List<AddonDto> GetAll(Guid Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Addons
                                 join c in context.AddonTypes on o.AddonTypeId equals c.Id
                                 where o.MenuId == Id 
                                 orderby o.Id descending
                                 select new AddonDto { AddonType = c.Name, AddonTypeId = o.AddonTypeId, Active = o.Active, Price = o.Price, MenuId = o.MenuId, Name = o.Name, CreatedDT = o.CreatedDt, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }    
    }
}


