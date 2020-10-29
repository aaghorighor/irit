namespace Suftnet.Cos.DataAccess
{
    using System;
    using DataAccess.Identity;
    using DataFactory.LinqToSql;
    using System.Linq;
    using System.Collections.Generic;

    public class User : IUser
    {
        public bool CheckEmailAddress(string email, Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = (from o in context.UserAccounts
                           join u in context.Users on o.UserId equals u.Id
                           where u.Email == email && o.TenantId == tenantId
                           select o).FirstOrDefault();

                return obj != null ? true : false;
            }
        }

        public bool CheckEmailAddress(string userName)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = context.Users.SingleOrDefault(o => o.UserName == userName);
                if (obj != null)
                {                   
                    return true;
                }
            }

            return false;
        }
        public ApplicationUser GetUserByPhone(string phoneNumber, Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = (from o in context.UserAccounts
                           join u in context.Users on o.UserId equals u.Id
                            where u.PhoneNumber == phoneNumber && o.TenantId == tenantId
                           select new { UserName = u.UserName, PhoneNumber = u.PhoneNumber, Active = u.Active, AreaId = u.AreaId, TenantId = o.TenantId, Id = u.Id }).FirstOrDefault();

                if (obj != null)
                {
                    return new ApplicationUser { UserName = obj.UserName, PhoneNumber = obj.PhoneNumber, Active = obj.Active, AreaId = obj.AreaId, Id = obj.Id, TenantId = tenantId };
                }

                return null;
            }
        }
        public ApplicationUser GetUserByUserName(string userName)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = (from o in context.UserAccounts
                           join u in context.Users on o.UserId equals u.Id
                           where u.UserName == userName
                           select new { UserName = u.UserName, PhoneNumber = u.PhoneNumber, Active = u.Active, AreaId = u.AreaId, TenantId = o.TenantId, Id = u.Id }).FirstOrDefault();

                if (obj != null)
                {
                    return new ApplicationUser { UserName = obj.UserName, PhoneNumber = obj.PhoneNumber, Active = obj.Active, AreaId = obj.AreaId, Id = obj.Id, TenantId = obj.TenantId };
                }

                return null;
            }
        }        
        public ApplicationUser GetUserByUserId(string userId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = (from o in context.UserAccounts
                           join u in context.Users on o.UserId equals u.Id
                           where o.UserId == userId
                           select new { FirstName = u.FirstName, LastName = u.LastName, UserName = u.UserName, PhoneNumber = u.PhoneNumber, Active = u.Active, AreaId = u.AreaId, TenantId = o.TenantId, Id = u.Id }).FirstOrDefault();

                if (obj != null)
                {
                    return new ApplicationUser { FirstName = obj.FirstName, LastName = obj.LastName, UserName = obj.UserName, PhoneNumber = obj.PhoneNumber, Active = obj.Active, AreaId = obj.AreaId, Id = obj.Id, TenantId = obj.TenantId };
                }

                return null;
            }
        }
        public ApplicationUser GetByUserId(string userId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = (from u in context.Users                        
                           where u.Id == userId
                           select u).FirstOrDefault();       

                return obj;
            }
        }

        public bool UpdateAccessCode(string phoneNumber, Guid tenantId, string otp)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = (from o in context.UserAccounts
                           join u in context.Users on o.UserId equals u.Id
                           where u.PhoneNumber == phoneNumber && o.TenantId == tenantId
                           select u).FirstOrDefault();

                if(obj != null)
                {
                    var objToUpdate = context.Users.SingleOrDefault(o => o.Id == obj.Id);
                    if (objToUpdate != null)
                    {
                        objToUpdate.OTP = otp;
                        context.SaveChanges();

                        return true;
                    }
                }                
            }

            return false;
        }
        public ApplicationUser VerifyAccessCode(string otp, string phoneNumber, Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = (from o in context.UserAccounts
                           join u in context.Users on o.UserId equals u.Id
                           where u.PhoneNumber == phoneNumber && o.TenantId == tenantId && u.OTP == otp
                           select new { UserName = u.UserName, PhoneNumber = u.PhoneNumber, Active = u.Active, AreaId = u.AreaId, TenantId = o.TenantId, Id = u.Id }).FirstOrDefault();

                if (obj != null)
                {
                    var objToUpdate = context.Users.SingleOrDefault(o => o.Id == obj.Id);
                    if (objToUpdate != null)
                    {
                        objToUpdate.OTP = "";
                        context.SaveChanges();

                        return new ApplicationUser { UserName = obj.UserName, PhoneNumber = obj.PhoneNumber, Active = obj.Active, AreaId = obj.AreaId, Id = obj.Id, TenantId = obj.TenantId };
                    }
                }               
            }

            return null;
        }
        public IList<UserAccountDto> GetById(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.UserAccounts
                                 join u in context.Users on o.UserId equals u.Id
                                 where o.TenantId == tenantId
                                 orderby o.Id ascending
                                 select new UserAccountDto { UserId = u.Id, ImageUrl = u.ImageUrl, Active = u.Active, TenantId = o.TenantId, Area = u.Area, AreaId = u.AreaId, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, Id =u.Id, UserName = u.UserName }).ToList();
                return objResult;
            }
        }
        public IList<UserAccountDto> GetAll(Guid tenantId, int iskip, int itake, string isearch)
        {
            if(!string.IsNullOrEmpty(isearch))
            {
                using (var context = DataContextFactory.CreateContext())
                {
                    var objResult = (from o in context.UserAccounts
                                     join u in context.Users on o.UserId equals u.Id
                                     where o.TenantId == tenantId && (u.FirstName.Contains(isearch) || u.LastName.Contains(isearch) || u.UserName.Contains(isearch))
                                     orderby o.Id ascending
                                     select new UserAccountDto { UserId = u.Id, ImageUrl = u.ImageUrl, Active = u.Active, Area = u.Area, AreaId = u.AreaId, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, Id = u.Id, UserName = u.UserName }).Skip(iskip).Take(itake).ToList();
                    return objResult;
                }
            }
            else
            {
                return GetAll(tenantId, iskip, itake);
            }           
        }
        public IList<UserAccountDto> GetAll(Guid tenantId, int iskip, int itake)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.UserAccounts
                                 join u in context.Users on o.UserId equals u.Id
                                 where o.TenantId == tenantId
                                 orderby o.Id ascending
                                 select new UserAccountDto { UserId = u.Id, ImageUrl = u.ImageUrl, Active = u.Active, Area = u.Area, AreaId = u.AreaId, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, Id = u.Id, UserName = u.UserName }).Skip(iskip).Take(itake).ToList();
                return objResult;
            }
        }
        public IList<UserAccountDto> GetAll(int iskip, int itake, string isearch)
        {
            if (!string.IsNullOrEmpty(isearch))
            {
                using (var context = DataContextFactory.CreateContext())
                {
                    var objResult = (from u in context.Users                                   
                                     where (u.FirstName.Contains(isearch) || u.LastName.Contains(isearch) || u.UserName.Contains(isearch))
                                     orderby u.Id ascending
                                     select new UserAccountDto { UserId = u.Id, ImageUrl = u.ImageUrl, Active = u.Active, Area = u.Area, AreaId = u.AreaId, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, Id = u.Id, UserName = u.UserName }).Skip(iskip).Take(itake).ToList();
                    return objResult;
                }
            }
            else
            {
                return GetAll(iskip, itake);
            }
        }
        public IList<UserAccountDto> GetAll(int iskip, int itake)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from u in context.Users                                                          
                                 orderby u.Id ascending
                                 select new UserAccountDto { UserId = u.Id, ImageUrl = u.ImageUrl, Active = u.Active, Area = u.Area, AreaId = u.AreaId, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, Id = u.Id, UserName = u.UserName }).Skip(iskip).Take(itake).ToList();
                return objResult;
            }
        }
        public IList<UserAccountDto> Fetch(int areaId, int iskip, int itake, string isearch)
        {
            if (!string.IsNullOrEmpty(isearch))
            {
                using (var context = DataContextFactory.CreateContext())
                {
                    var objResult = (from u in context.Users
                                     where u.AreaId == areaId && (u.FirstName.Contains(isearch) || u.LastName.Contains(isearch) || u.UserName.Contains(isearch))
                                     orderby u.Id descending
                                     select new UserAccountDto { UserId = u.Id, ImageUrl = u.ImageUrl, Active = u.Active, Area = u.Area, AreaId = u.AreaId, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, Id = u.Id, UserName = u.UserName }).Skip(iskip).Take(itake).ToList();
                    return objResult;
                }
            }
            else
            {
                return Fetch(areaId, iskip, itake);
            }
        }

        public IList<UserAccountDto> Fetch(int areaId, int iskip, int itake)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from u in context.Users
                                 where u.AreaId == areaId
                                 orderby u.Id descending
                                 select new UserAccountDto { UserId = u.Id, ImageUrl = u.ImageUrl, Active = u.Active, Area = u.Area, AreaId = u.AreaId, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, Id = u.Id, UserName = u.UserName }).Skip(iskip).Take(itake).ToList();
                return objResult;
            }
        }
        public int Count(Guid TenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.UserAccounts
                                 join u in context.Users on o.UserId equals u.Id
                                 where o.TenantId == TenantId
                                 select u).Count();

                return objResult;
            }
        }
        public int Count()
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = (from o in context.UserAccounts                   
                                 select o).Count();
                return obj;
            }
        }
    }
}