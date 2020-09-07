namespace Suftnet.Cos.DataAccess
{   
    using System.Collections.Generic;
    using System.Linq;

    using Suftnet.DataFactory.LinqToSql;
    using System.Data.Linq;
    using System;

    public class Permission : IPermission
    {        
        public bool Delete(Guid id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.Permissions.FirstOrDefault(o => o.Id == id);
                if (objToDelete != null)
                {
                    context.Permissions.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }
            return response;           
        }

        public bool Clear(string userId)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.Permissions.Where(o => o.UserId == userId).ToList();
                if (objToDelete.Any())
                {
                    context.Permissions.RemoveRange(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }
            return response;
        }

        public List<PermissionDto> GetByUserId(string userId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Permissions
                                 join c in context.Commons on o.ViewId equals c.ID
                                 where o.UserId == userId
                                 orderby o.ViewId
                                 select new PermissionDto { IdentityId = o.UserId, View = c.Title, CreatedDT= o.CreatedDt, Edit = o.Edit, Custom = o.Custom, Get = o.Get, GetAll = o.GetAll, Create = o.Create, ViewId = o.ViewId, Remove = o.Remove, Id = o.Id }).ToList();
                return objResult;
            }
        }      
        
      
        public PermissionDto Get(Guid id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Permissions
                                 join c in context.Commons on o.ViewId equals c.ID
                                 where o.Id == id
                                 select new PermissionDto { IdentityId = o.UserId, View = c.Title, CreatedDT= o.CreatedDt, Edit = o.Edit, Custom = o.Custom, Get = o.Get, GetAll = o.GetAll, Create = o.Create, ViewId = o.ViewId, Remove = o.Remove, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }

        public PermissionDto Match(int viewId, string userId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Permissions                              
                                 where o.ViewId == viewId && o.UserId == userId
                                 select new PermissionDto { IdentityId = o.UserId, CreatedDT = o.CreatedDt, Edit = o.Edit, Custom = o.Custom, Get = o.Get, GetAll = o.GetAll, Create = o.Create, ViewId = o.ViewId, Remove = o.Remove, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }

        public Guid Insert(PermissionDto entity)
        {
            Guid id;
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.Permission() { Id = entity.Id, UserId = entity.UserId, Edit = entity.Edit, Custom = entity.Custom, Get = entity.Get, GetAll = entity.GetAll, Create = entity.Create, ViewId = entity.ViewId, Remove = entity.Remove, CreatedBy = entity.CreatedBy, CreatedDt = entity.CreatedDT };
                context.Permissions.Add(obj);
                context.SaveChanges();
                id = obj.Id;
            }
            return id;
        }

        public bool Update(PermissionDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Permissions.SingleOrDefault(o => o.Id == entity.Id);
                if (objToUpdate != null)
                {
                    objToUpdate.Edit = entity.Edit;
                    objToUpdate.Custom = entity.Custom;
                    objToUpdate.Get = entity.Get;
                    objToUpdate.GetAll = entity.GetAll;
                    objToUpdate.Create = entity.Create;
                    objToUpdate.ViewId = entity.ViewId;
                    objToUpdate.Remove = entity.Remove;
                   
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
    }
}
