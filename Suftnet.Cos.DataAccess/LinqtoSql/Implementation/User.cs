namespace Suftnet.Cos.DataAccess
{
    using System;
    using DataAccess.Identity;
    using DataFactory.LinqToSql;
    using System.Linq;
    using System.Collections.Generic;

    public class User : IUser
    {
        private readonly ITenant _tenant;
        public User(ITenant tenant)
        {
            _tenant = tenant;
        }

        public bool Delete(string userId)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var userAccounts = context.UserAccounts.Where(o => o.UserId == userId);
                if (userAccounts.Any())
                {
                    context.UserAccounts.RemoveRange(userAccounts);
                    context.SaveChanges();
                }

                var permissions = context.Permissions.Where(o => o.UserId == userId);
                if (permissions.Any())
                {
                    context.Permissions.RemoveRange(permissions);
                    context.SaveChanges();
                }

                var mobilePermissions = context.MobilePermissions.Where(o => o.UserId == userId);
                if (mobilePermissions.Any())
                {
                    context.MobilePermissions.RemoveRange(mobilePermissions);
                    context.SaveChanges();
                }

                var users = context.Users.SingleOrDefault(o => o.Id == userId);
                if (users != null)
                {
                    context.Users.Remove(users);
                    context.SaveChanges();
                }

                response = true;
            }

            return response;
        }
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
        public ApplicationUser GetUserByUserName(string userName, string appCode)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = (from o in context.UserAccounts
                           join u in context.Users on o.UserId equals u.Id
                           where o.EmailAddress == userName && o.AppCode == appCode
                           select new { UserName = u.UserName, PhoneNumber = u.PhoneNumber, Active = u.Active, AreaId = u.AreaId, TenantId = o.TenantId, Id = u.Id }).FirstOrDefault();

                if (obj != null)
                {
                    return new ApplicationUser { UserName = obj.UserName, PhoneNumber = obj.PhoneNumber, Active = obj.Active, AreaId = obj.AreaId, Id = obj.Id, TenantId = obj.TenantId };
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
        public IList<UserAccountDto> GetById(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.UserAccounts
                                 join u in context.Users on o.UserId equals u.Id
                                 where o.TenantId == tenantId
                                 orderby o.Id ascending
                                 select new UserAccountDto { UserCode = o.UserCode, UserId = u.Id, ImageUrl = u.ImageUrl, Active = u.Active, TenantId = o.TenantId, Area = u.Area, AreaId = u.AreaId, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, Id =u.Id, UserName = u.UserName }).ToList();
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
                                     select new UserAccountDto { UserCode = o.UserCode, UserId = u.Id, ImageUrl = u.ImageUrl, Active = u.Active, Area = u.Area, AreaId = u.AreaId, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, Id = u.Id, UserName = u.UserName }).Skip(iskip).Take(itake).ToList();
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
                                 select new UserAccountDto { UserCode = o.UserCode, UserId = u.Id, ImageUrl = u.ImageUrl, Active = u.Active, Area = u.Area, AreaId = u.AreaId, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, Id = u.Id, UserName = u.UserName }).Skip(iskip).Take(itake).ToList();
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
                                     select new UserAccountDto { UserCode = u.UserCode, UserId = u.Id, ImageUrl = u.ImageUrl, Active = u.Active, Area = u.Area, AreaId = u.AreaId, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, Id = u.Id, UserName = u.UserName }).Skip(iskip).Take(itake).ToList();
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
                                 select new UserAccountDto { UserCode = u.UserCode, UserId = u.Id, ImageUrl = u.ImageUrl, Active = u.Active, Area = u.Area, AreaId = u.AreaId, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, Id = u.Id, UserName = u.UserName }).Skip(iskip).Take(itake).ToList();
                return objResult;
            }
        }
        public IList<UserAccountDto> Fetch(int iskip, int itake, string isearch, params int[] areaId)
        {
            if (!string.IsNullOrEmpty(isearch))
            {
                using (var context = DataContextFactory.CreateContext())
                {
                    var objResult = (from u in context.Users
                                     where areaId.Contains(u.AreaId) && (u.FirstName.Contains(isearch) || u.LastName.Contains(isearch) || u.UserName.Contains(isearch))
                                     orderby u.Id descending
                                     select new UserAccountDto { UserCode = u.UserCode, UserId = u.Id, ImageUrl = u.ImageUrl, Active = u.Active, Area = u.Area, AreaId = u.AreaId, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, Id = u.Id, UserName = u.UserName }).Skip(iskip).Take(itake).ToList();
                    return objResult;
                }
            }
            else
            {
                return Fetch(iskip, itake, areaId);
            }
        }
        public IList<UserAccountDto> Fetch(int iskip, int itake, params int[] areaId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from u in context.Users
                                 where areaId.Contains(u.AreaId)
                                 orderby u.Id descending
                                 select new UserAccountDto { UserCode = u.UserCode, UserId = u.Id, ImageUrl = u.ImageUrl, Active = u.Active, Area = u.Area, AreaId = u.AreaId, FirstName = u.FirstName, LastName = u.LastName, Email = u.Email, Id = u.Id, UserName = u.UserName }).Skip(iskip).Take(itake).ToList();
                return objResult;
            }
        }
        public bool UpdateAccessCode(string userId, string otp)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Users.SingleOrDefault(o => o.Id == userId);
                if (objToUpdate != null)
                {
                    objToUpdate.OTP = otp;
                    context.SaveChanges();

                    return true;
                }
            }

            return false;
        }
        public MobileTenantDto VerifyAccessCode(string otp, string emailAddress, string appCode)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = (from o in context.UserAccounts.Include("Tenants").Include("TenantAddresses")
                           join u in context.Users on o.UserId equals u.Id
                           let t=o.Tenants
                           let a = o.Tenants.TenantAddresses
                           where o.EmailAddress == emailAddress && o.AppCode == appCode && u.OTP == otp
                           select new MobileTenantDto {  TaxRate = t.TaxRate, DeliveryCost = t.DeliveryCost, DiscountRate = t.DiscountRate, CurrencySymbol = t.CurrencyCode, CompleteAddress = a.CompleteAddress, Country = a.Country, Town = a.Town, PostCode = a.PostCode, Description = t.Description, Email =t.Email, ImageUrl = u.ImageUrl, FirstName = u.FirstName, LastName =u.LastName, Name = t.Name,
                              AreaId =u.AreaId, LogoUrl = t.LogoUrl, WebsiteUrl = t.WebsiteUrl, Telephone = t.Telephone, Latitude = a.Latitude, Longitude = a.Logitude, Mobile = t.Mobile, Area =u.Area,  UserName = u.UserName, PhoneNumber = u.PhoneNumber, TenantId =o.TenantId, Id = u.Id }).FirstOrDefault();

                if (obj != null)
                {
                    var objToUpdate = context.Users.SingleOrDefault(o => o.Id == obj.Id);
                    if (objToUpdate != null)
                    {
                        objToUpdate.OTP = "";
                        context.SaveChanges();

                        return obj;
                    }
                }
            }

            return null;
        }
        public MobileTenantDto VerifyUser(Guid tenantId, string userCode)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = (from o in context.UserAccounts
                           join u in context.Users on o.UserId equals u.Id                          
                           where o.TenantId == tenantId && o.UserCode == userCode 
                           select new MobileTenantDto
                           {
                               TenantId = o.TenantId,
                               Email = u.Email,                           
                               FirstName = u.FirstName,
                               LastName = u.LastName,                              
                               AreaId = u.AreaId,                           
                               Area = u.Area,
                               UserName = u.UserName,
                               PhoneNumber = u.PhoneNumber, 
                               Id =u.Id
                           }).FirstOrDefault();

                return obj;
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