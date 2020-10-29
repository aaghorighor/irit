namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
    using System.Data.Linq;

    public class Tenant : ITenant
    {       
        public TenantDto Get(Guid Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tenants.Include("TenantAddresses").Include("TenantStates")
                                 let s = o.TenantStates
                                 let a = o.TenantAddresses
                                 where o.Id == Id
                                 select new TenantDto { FlatRate = o.FlatRate, DeliveryLimitNote = o.DeliveryLimitNote, IsFlatRate = o.IsFlatRate, StatusId = s.Id, DeliveryRate = o.DeliveryRate, DeliveryUnitId = o.DeliveryUnitId, Description = o.Description, BackgroundUrl = o.BackgroundUrl, CurrencyCode = o.CurrencyCode, CompleteAddress = a.CompleteAddress, Latitude = a.Latitude, Longitude = a.Logitude, AddressLine3 = a.AddressLine3, AddressLine2 = a.AddressLine2, AddressLine1 = a.AddressLine1, Country = a.Country, County = a.County, Town = a.Town, PostCode = a.PostCode, SubscriptionId = o.SubscriptionId, StripeSecretKey = o.StripeSecretKey, StripePublishableKey = o.StripePublishableKey, Publish = o.Publish, LogoUrl = o.LogoUrl, TenantId = o.Id, IsExpired = (bool)o.IsExpired, PlanTypeId = o.PlanTypeId, WebsiteUrl = o.WebsiteUrl, Status = s.Name, StartDate = o.StartDate, Startup = o.Startup, CurrencyId = o.CurrencyId, CustomerStripeId = o.CustomerStripeId, ExpirationDate = o.ExpirationDate, Telephone = o.Telephone, Name = o.Name, Email = o.Email, Mobile = o.Mobile, AddressId = a.Id, CreatedDT = o.CreatedDt, Id = o.Id }).FirstOrDefault();
                return objResult;                
            }          
        }

        public int Status(Guid statusId, Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tenants
                                 where o.StateId == statusId && o.Id != tenantId
                                 select o).Count();

                return objResult;
            }
        }
        public TenantDto Expired(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = (from o in context.Tenants
                           where o.Id == tenantId
                           select new TenantDto { IsExpired = (bool)o.IsExpired, PlanTypeId = o.PlanTypeId }).FirstOrDefault();
                return obj;
            }
        }

        public TenantDto IsValid(string customerStripId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tenants                               
                                 where o.CustomerStripeId == customerStripId
                                 select new TenantDto { SubscriptionId = o.SubscriptionId, TenantId = o.Id, IsExpired = (bool)o.IsExpired, PlanTypeId = o.PlanTypeId, StartDate = o.StartDate, CustomerStripeId = o.CustomerStripeId, ExpirationDate = o.ExpirationDate, Telephone = o.Telephone, Name = o.Name, Email = o.Email, Mobile = o.Mobile, AddressId = o.AddressId, CreatedDT= o.CreatedDt, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }       
        public bool IsTenantNew(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tenants
                                 where o.Id == tenantId
                                 select o).FirstOrDefault();

                return objResult != null ? true : false;
            }
        }           
        public bool Delete(Guid Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.Tenants.FirstOrDefault(o => o.Id == Id);
                if (objToDelete != null)
                {
                    context.Tenants.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }
            return response;
        }
        public Guid Insert(TenantDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.Tenant()
                {
                    CurrencyCode =entity.CurrencyCode, SubscriptionId = entity.SubscriptionId,
                    DeliveryRate = entity.DeliveryRate,
                    DeliveryUnitId = entity.DeliveryUnitId,
                    BackgroundUrl = entity.BackgroundUrl,Description = entity.Description,                 
                    StripeSecretKey = entity.StripeSecretKey,
                    StripePublishableKey = entity.StripePublishableKey, Publish = entity.Publish, LogoUrl = entity.LogoUrl,
                    StartDate = entity.StartDate, PlanTypeId = entity.PlanTypeId,
                    IsExpired = entity.IsExpired,
                    WebsiteUrl = entity.WebsiteUrl, FlatRate= entity.FlatRate,                 
                    Startup = entity.Startup, CurrencyId = entity.CurrencyId,
                    ExpirationDate = entity.ExpirationDate, CustomerStripeId = entity.CustomerStripeId, Telephone = entity.Telephone,
                    Mobile = entity.Mobile, Email = entity.Email, Name = entity.Name, AddressId = entity.AddressId, StateId = entity.StatusId,
                    CreatedBy = entity.CreatedBy, CreatedDt = entity.CreatedDT, Id = entity.Id, DeliveryLimitNote = entity.DeliveryLimitNote, IsFlatRate = entity.IsFlatRate
                };

                context.Tenants.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }
        public bool AdminUpdate(TenantDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Tenants.SingleOrDefault(o => o.Id == entity.Id);
                if (objToUpdate != null)
                {
                    objToUpdate.CurrencyCode = entity.CurrencyCode;
                    objToUpdate.LogoUrl = entity.LogoUrl;              
                    objToUpdate.Publish = entity.Publish;                   
                    objToUpdate.Telephone = entity.Telephone;
                    objToUpdate.LogoUrl = entity.LogoUrl;
                    objToUpdate.CurrencyId = entity.CurrencyId;
                    objToUpdate.Mobile = entity.Mobile;
                    objToUpdate.Email = entity.Email;
                    objToUpdate.Name = entity.Name;
                    objToUpdate.AddressId = entity.AddressId;
                    objToUpdate.StateId = entity.StatusId;
                    objToUpdate.DeliveryRate = entity.DeliveryRate;
                    objToUpdate.DeliveryUnitId = entity.DeliveryUnitId;
                    objToUpdate.FlatRate = entity.FlatRate;
                    objToUpdate.IsFlatRate = entity.IsFlatRate;
                    objToUpdate.DeliveryLimitNote = entity.DeliveryLimitNote;
                    objToUpdate.WebsiteUrl = entity.WebsiteUrl;
                    objToUpdate.Description = entity.Description;
                    objToUpdate.StripeSecretKey = entity.StripeSecretKey;
                    objToUpdate.StripePublishableKey = entity.StripePublishableKey;                   

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
        public bool UpdateStatus(Guid tenantId, bool isExpired)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Tenants.SingleOrDefault(o => o.Id == tenantId);
                if (objToUpdate != null)
                {
                    objToUpdate.IsExpired = isExpired;                  
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
        public bool UpdateStartUp(Guid tenantId, bool status)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Tenants.SingleOrDefault(o => o.Id == tenantId);
                if (objToUpdate != null)
                {
                    objToUpdate.Startup = status;

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
        public bool UpdateCustomer(TenantDto tenant)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Tenants.SingleOrDefault(o => o.Id == tenant.Id);
                if (objToUpdate != null)
                {
                    objToUpdate.StartDate = tenant.StartDate;
                    objToUpdate.ExpirationDate = tenant.ExpirationDate;
                    objToUpdate.IsExpired = tenant.IsExpired;
                    objToUpdate.SubscriptionId = tenant.SubscriptionId;
                    objToUpdate.CustomerStripeId = tenant.CustomerStripeId;
                    objToUpdate.PlanTypeId = tenant.PlanTypeId;

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
        public bool Update(TenantDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Tenants.SingleOrDefault(o => o.Id == entity.Id);
                if (objToUpdate != null)
                {
                    objToUpdate.CurrencyCode = entity.CurrencyCode;
                    objToUpdate.LogoUrl = entity.LogoUrl;                   
                    objToUpdate.Publish = entity.Publish;               
                    objToUpdate.Telephone = entity.Telephone;                          
                    objToUpdate.LogoUrl = entity.LogoUrl;                    
                    objToUpdate.CurrencyId = entity.CurrencyId;
                    objToUpdate.Mobile = entity.Mobile;
                    objToUpdate.Email = entity.Email;
                    objToUpdate.Name = entity.Name;
                    objToUpdate.AddressId = entity.AddressId;
                    objToUpdate.StateId = entity.StatusId;
                    objToUpdate.DeliveryRate = entity.DeliveryRate;
                    objToUpdate.DeliveryUnitId = entity.DeliveryUnitId;
                    objToUpdate.IsFlatRate = entity.IsFlatRate;
                    objToUpdate.FlatRate = entity.FlatRate;
                    objToUpdate.DeliveryLimitNote = entity.DeliveryLimitNote;
                    objToUpdate.WebsiteUrl = entity.WebsiteUrl;
                    objToUpdate.Description = entity.Description;
                    objToUpdate.StripeSecretKey = entity.StripeSecretKey;
                    objToUpdate.StripePublishableKey = entity.StripePublishableKey;
                    
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
        public bool CancelTrial(RequestDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Tenants.SingleOrDefault(o => o.Id == entity.TenantId);
                if (objToUpdate != null)
                {
                    objToUpdate.ExpirationDate = entity.EndDate;
                  
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

        public TenantAdapter GetAll(int iskip, int itake, string terms)
        {

            if (!string.IsNullOrEmpty(terms))
            {
                using (var context = DataContextFactory.CreateContext())
                {
                    var obj = (from o in context.Tenants
                               join s in context.TenantStates on o.StateId equals s.Id
                               where o.Name.Contains(terms) || s.Name.Contains(terms)
                               orderby o.Name
                               select new TenantDto { ExpirationDate = o.ExpirationDate, CustomerStripeId = o.CustomerStripeId, Status = s.Name, Telephone = o.Telephone, Name = o.Name, Email = o.Email, Mobile = o.Mobile, CreatedDT = o.CreatedDt, Id = o.Id }).Skip(iskip).Take(itake).ToList();

                    return new TenantAdapter { Count = Count(), TenantDto = obj };
                }
            }
            else
            {
                return GetAll(iskip, itake);
            }

        }

        public TenantAdapter GetAll(int iskip, int itake)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = (from o in context.Tenants
                           join s in context.TenantStates on o.StateId equals s.Id
                           orderby o.Name
                           select new TenantDto { ExpirationDate = o.ExpirationDate, CustomerStripeId = o.CustomerStripeId, Status = s.Name, Telephone = o.Telephone, Name = o.Name, Email = o.Email, Mobile = o.Mobile, CreatedDT = o.CreatedDt, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                return new TenantAdapter { Count = Count(), TenantDto = obj };
            }
        }

        public List<TenantShortDto> GetAll(int iskip, int itake, bool status)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tenants.Include("TenantAddresses").Include("TenantStates")
                                 let s = o.TenantStates
                                 let a = o.TenantAddresses
                                 where o.Publish == status
                                 orderby o.Name
                                 select new TenantShortDto {  CompleteAddress = a.CompleteAddress, Latitude = a.Latitude, Longitude = a.Logitude, AddressLine3 = a.AddressLine3, AddressLine2 = a.AddressLine2, AddressLine1 = a.AddressLine1, Country = a.Country, County = a.County, Town = a.Town, PostCode = a.PostCode, LogoUrl = o.LogoUrl, Name = o.Name, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                return objResult;
            }
        }                                     
        public int Count(bool status)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tenants
                                 where o.IsExpired == status
                                 select o).Count();
                return objResult;
            }
        }

        public int Count()
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tenants                               
                                 select o).Count();
                return objResult;
            }
        }
    }
}
