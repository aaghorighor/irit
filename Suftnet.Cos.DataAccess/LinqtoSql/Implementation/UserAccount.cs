namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;     
    using DataFactory.LinqToSql;

    class UserAccount : IUserAccount
    {       
        public bool Delete(Guid tenantId)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.UserAccounts.SingleOrDefault(o => o.TenantId == tenantId);
                if (objToDelete != null)
                {
                    context.UserAccounts.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }

            return response;
        }

        public List<UserAccountDto> GetById(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.UserAccounts
                                 join u in context.Users on o.UserId equals u.Id
                                 where o.TenantId == tenantId
                                 orderby o.Id descending
                                 select new UserAccountDto { UserId = u.Id, Id = u.Id, AreaId = u.AreaId,  Area = u.Area, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, UserName = u.UserName, Active =u.Active, TenantId = o.TenantId }).ToList();
                return objResult;
            }
        }

        public UserAccountDto GetByUserName(string userName)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.UserAccounts
                                 join u in context.Users on o.UserId equals u.Id
                                 where u.UserName == userName                               
                                 select new UserAccountDto { UserId = u.Id, Id = u.Id, AreaId = u.AreaId, Area = u.Area, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, UserName = u.UserName, Active = u.Active, TenantId = o.TenantId }).FirstOrDefault();
                return objResult;
            }
        }

        public UserAccountDto GetByUserId(string userId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.UserAccounts
                                 join u in context.Users on o.UserId equals u.Id
                                 join t in context.Tenants on o.TenantId equals t.Id
                                 where o.UserId == userId
                                 select new UserAccountDto { ExpirationDate = t.ExpirationDate, IsExpired = t.IsExpired, TenantName = t.Name, UserId = u.Id, Id = u.Id, AreaId = u.AreaId, Area = u.Area, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, UserName = u.UserName, Active = u.Active, TenantId = o.TenantId }).FirstOrDefault();
                return objResult;
            }
        }

        public int Insert(Action.UserAccount entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.UserAccount() { TenantId = entity.TenantId, UserId= entity.UserId, CreatedBy = entity.CreatedBy, CreatedDt = entity.CreatedDt };
                context.UserAccounts.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }
    }
}
