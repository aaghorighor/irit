namespace Suftnet.Cos.DataAccess
{
    using System.Collections.Generic;
    using System.Linq;

    using Suftnet.DataFactory.LinqToSql;
    using System.Data.Linq;
    using System;

    public class MobilePermission : IMobilePermission
    {        
        public bool Delete(Guid id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.MobilePermissions.FirstOrDefault(o => o.Id == id);
                if (objToDelete != null)
                {
                    context.MobilePermissions.Remove(objToDelete);
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
                var objToDelete = context.MobilePermissions.Where(o => o.UserId == userId).ToList();
                if (objToDelete.Any())
                {
                    context.MobilePermissions.RemoveRange(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }
            return response;
        }

        public List<MobilePermissionDto> GetByUserId(string userId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.MobilePermissions
                                 join c in context.Commons on o.PermissionId equals c.ID
                                 where o.UserId == userId
                                 orderby o.Id
                                 select new MobilePermissionDto { CreatedBy = o.CreatedBy, Description = c.Title, CreatedDT= o.CreatedDt, PermissionId = o.PermissionId,  UserId = o.UserId, Id = o.Id }).ToList();
                return objResult;
            }
        }

        public string[] GetPermissionByUserId(string userId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.MobilePermissions                            
                                 where o.UserId == userId
                                 orderby o.Id
                                 select o.PermissionId.ToString()).ToArray();
                return objResult;
            }
        }

        public bool Find(MobilePermissionDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.MobilePermissions                              
                                 where o.PermissionId == entity.PermissionId && o.UserId == entity.UserId                                
                                 select o).FirstOrDefault();
                return objResult != null ? true : false;
            }
        }

        public MobilePermissionDto Get(Guid id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.MobilePermissions
                                 join c in context.Commons on o.PermissionId equals c.ID
                                 where o.Id == id
                                 orderby o.Id
                                 select new MobilePermissionDto { Description = c.Title, CreatedBy = o.CreatedBy, CreatedDT= o.CreatedDt, PermissionId = o.PermissionId, UserId = o.UserId, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }       

        public Guid Insert(MobilePermissionDto entity)
        {
            Guid id;
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.MobilePermission() { Id = entity.Id, UserId = entity.UserId, PermissionId = entity.PermissionId, CreatedBy = entity.CreatedBy, CreatedDt = entity.CreatedDT };
                context.MobilePermissions.Add(obj);
                context.SaveChanges();
                id = obj.Id;
            }
            return id;
        }

        public bool Update(MobilePermissionDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.MobilePermissions.SingleOrDefault(o => o.Id == entity.Id);
                if (objToUpdate != null)
                {
                    objToUpdate.PermissionId = entity.PermissionId;
                    objToUpdate.UserId = entity.UserId;
                   
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
