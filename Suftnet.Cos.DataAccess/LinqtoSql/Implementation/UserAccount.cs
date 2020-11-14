namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
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
                var objResult = (from o in context.UserAccounts.Include("Tenants").Include("TenantAddresses")
                                 join u in context.Users on o.UserId equals u.Id
                                 let t = o.Tenants
                                 where o.EmailAddress == userName
                                 select new UserAccountDto { TenantEmail = u.Email, TenantMobile = u.PhoneNumber, DeliveryLimitNote = t.DeliveryLimitNote, FlatRate = t.FlatRate, IsFlatRate = t.IsFlatRate, CurrencyCode = t.CurrencyCode, DeliveryRate = t.DeliveryRate, DeliveryUnit = t.DeliveryUnitId, CompleteAddress = t.TenantAddresses.CompleteAddress, ExpirationDate = t.ExpirationDate, IsExpired = t.IsExpired, TenantName = t.Name, UserId = u.Id, Id = u.Id, AreaId = u.AreaId, Area = u.Area, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, UserName = u.UserName, Active = u.Active, TenantId = o.TenantId }).FirstOrDefault();
                return objResult;
            }
        }

        public UserAccountDto GetByUserId(string userId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.UserAccounts.Include("Tenants").Include("TenantAddresses")
                                 join u in context.Users on o.UserId equals u.Id   
                                 let t = o.Tenants
                                 where o.UserId == userId
                                 select new UserAccountDto { TenantEmail =u.Email, TenantMobile = u.PhoneNumber, DeliveryLimitNote = t.DeliveryLimitNote, FlatRate=t.FlatRate, IsFlatRate= t.IsFlatRate, CurrencyCode = t.CurrencyCode, DeliveryRate =t.DeliveryRate, DeliveryUnit = t.DeliveryUnitId, CompleteAddress = t.TenantAddresses.CompleteAddress, ExpirationDate = t.ExpirationDate, IsExpired = t.IsExpired, TenantName = t.Name, UserId = u.Id, Id = u.Id, AreaId = u.AreaId, Area = u.Area, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, UserName = u.UserName, Active = u.Active, TenantId = o.TenantId }).FirstOrDefault();
                return objResult;
            }
        }

        public int Insert(Action.UserAccount entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.UserAccount() { EmailAddress = entity.EmailAddress, AppCode = entity.AppCode, TenantId = entity.TenantId, UserId= entity.UserId, CreatedBy = entity.CreatedBy, CreatedDt = entity.CreatedDt };
                context.UserAccounts.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }
    }
}
