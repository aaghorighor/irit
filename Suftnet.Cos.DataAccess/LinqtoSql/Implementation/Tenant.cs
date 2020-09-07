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
                var objResult = (from o in context.Tenants
                                 join s in context.TenantStatus on o.StatusId equals s.Id                                 
                                 join a in context.TenantAddress on o.AddressId equals a.Id                                
                                 where o.Id == Id
                                 select new TenantDto { Description = o.Description, BackgroundUrl = o.BackgroundUrl, CurrencyCode = o.CurrencyCode, CompleteAddress = a.CompleteAddress, Latitude = a.Latitude, Longitude =a.Logitude, AddressLine3 = a.AddressLine3, AddressLine2 = a.AddressLine2, AddressLine1 =a.AddressLine1, Country = a.Country, County = a.County, Town = a.Town, PostCode = a.PostCode, SubscriptionId = o.SubscriptionId, StripeSecretKey = o.StripeSecretKey, StripePublishableKey = o.StripePublishableKey, Publish = o.Publish, LogoUrl = o.LogoUrl, TenantId = o.Id, IsExpired =(bool)o.IsExpired, PlanTypeId = o.PlanTypeId, WebsiteUrl = o.WebsiteUrl, Status = s.Name, StartDate = o.StartDate, Startup = o.Startup, CurrencyId = o.CurrencyId, CustomerStripeId = o.CustomerStripeId, ExpirationDate = o.ExpirationDate, StatusId = o.StatusId, Telephone = o.Telephone, Name = o.Name, Email = o.Email, Mobile = o.Mobile, AddressId = o.AddressId, CreatedDT= o.CreatedDt, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }
        public TenantDto IsValid(string customerStripId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tenants                               
                                 where o.CustomerStripeId == customerStripId
                                 select new TenantDto { SubscriptionId = o.SubscriptionId, TenantId = o.Id, IsExpired = (bool)o.IsExpired, PlanTypeId = o.PlanTypeId, StartDate = o.StartDate, CustomerStripeId = o.CustomerStripeId, ExpirationDate = o.ExpirationDate, StatusId = o.StatusId, Telephone = o.Telephone, Name = o.Name, Email = o.Email, Mobile = o.Mobile, AddressId = o.AddressId, CreatedDT= o.CreatedDt, Id = o.Id }).FirstOrDefault();
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
                    BackgroundUrl = entity.BackgroundUrl,Description = entity.Description,                 
                    StripeSecretKey = entity.StripeSecretKey,
                    StripePublishableKey = entity.StripePublishableKey, Publish = entity.Publish, LogoUrl = entity.LogoUrl,
                    StartDate = entity.StartDate, PlanTypeId = entity.PlanTypeId,
                    IsExpired = entity.IsExpired,
                    WebsiteUrl = entity.WebsiteUrl,                 
                    Startup = entity.Startup, CurrencyId = entity.CurrencyId,
                    ExpirationDate = entity.ExpirationDate, CustomerStripeId = entity.CustomerStripeId, Telephone = entity.Telephone,
                    AddressId = entity.AddressId, Mobile = entity.Mobile, Email = entity.Email, Name = entity.Name,
                    CreatedBy = entity.CreatedBy, CreatedDt = entity.CreatedDT, Id = entity.Id, StatusId = entity.StatusId

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
                    objToUpdate.StatusId = entity.StatusId;

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
        public List<TenantDto> GetAll()
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tenants                                                      
                                 join t in context.TenantStatus on o.StatusId equals t.Id
                                 join a in context.TenantAddress on o.AddressId equals a.Id
                                 orderby o.Id descending
                                 select new TenantDto { CompleteAddress = a.CompleteAddress, Latitude = a.Latitude, Longitude = a.Logitude, AddressLine3 = a.AddressLine3, AddressLine2 = a.AddressLine2, AddressLine1 = a.AddressLine1, Country = a.Country, County = a.County, Town = a.Town, PostCode = a.PostCode, Startup = o.Startup, CustomerStripeId= o.CustomerStripeId, Status =t.Name, StatusId = o.StatusId, Telephone = o.Telephone, Name = o.Name, Email = o.Email, Mobile = o.Mobile, AddressId = o.AddressId, CreatedDT= o.CreatedDt, Id = o.Id }).ToList();
                return objResult;
            }
        }
        public List<TenantDto> GetAll(int statusId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tenants
                                 join a in context.TenantAddress on o.AddressId equals a.Id
                                 where o.StatusId == o.StatusId 
                                 orderby o.Id descending
                                 select new TenantDto { TenantId = o.Id, LogoUrl = o.LogoUrl, CompleteAddress = a.CompleteAddress, Latitude = a.Latitude, Longitude = a.Logitude, AddressLine3 = a.AddressLine3, AddressLine2 = a.AddressLine2, AddressLine1 = a.AddressLine1, Country = a.Country, County = a.County, Town = a.Town, PostCode = a.PostCode,Telephone = o.Telephone, Name = o.Name, Email = o.Email, Mobile = o.Mobile, Id = o.Id }).ToList();
                return objResult;
            }
        }
        public List<TenantDto> Startup(int statusId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tenants
                                 join a in context.TenantAddress on o.AddressId equals a.Id
                                 where o.StatusId == o.StatusId && o.Publish == true
                                 orderby o.Id descending
                                 select new TenantDto { TenantId = o.Id, LogoUrl = o.LogoUrl, CompleteAddress = a.CompleteAddress, Latitude = a.Latitude, Longitude = a.Logitude, AddressLine3 = a.AddressLine3, AddressLine2 = a.AddressLine2, AddressLine1 = a.AddressLine1, Country = a.Country, County = a.County, Town = a.Town, PostCode = a.PostCode,Telephone = o.Telephone, Name = o.Name, Email = o.Email, Mobile = o.Mobile, Id = o.Id }).Take(10).ToList();
                return objResult;
            }
        }
        public List<TenantShortDto> GetByFilterByPostCode(string postcode)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tenants                                
                                 join a in context.TenantAddress on o.AddressId equals a.Id                             
                                 where (o.Publish == true) && (a.PostCode == postcode)
                                 orderby o.Name
                                 select new TenantShortDto { CompleteAddress = a.CompleteAddress, Latitude = a.Latitude, Longitude = a.Logitude, AddressLine3 = a.AddressLine3, AddressLine2 = a.AddressLine2, AddressLine1 = a.AddressLine1, Country = a.Country, County = a.County, Town = a.Town, PostCode = a.PostCode, LogoUrl = o.LogoUrl, Name = o.Name, Id = o.Id }).ToList();
                return objResult;
            }
        }
        public List<TenantShortDto> GetByFilterByCoordinate(string latitude, string longitude)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tenants                       
                                 join a in context.TenantAddress on o.AddressId equals a.Id                               
                                 where (o.Publish == true) && (a.Latitude == latitude && a.Logitude == longitude)
                                 orderby o.Name
                                 select new TenantShortDto { CompleteAddress = a.CompleteAddress, Latitude = a.Latitude, Longitude = a.Logitude, AddressLine3 = a.AddressLine3, AddressLine2 = a.AddressLine2, AddressLine1 = a.AddressLine1, Country = a.Country, County = a.County, Town = a.Town, PostCode = a.PostCode, LogoUrl = o.LogoUrl, Name = o.Name, Id = o.Id }).ToList();
                return objResult;
            }
        }
        public List<TenantShortDto> GetByFilterByAddress(string completeAddress)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tenants                          
                                 join a in context.TenantAddress on o.AddressId equals a.Id                              
                                 where (o.Publish == true) && (a.CompleteAddress.Contains(completeAddress))
                                 orderby o.Name
                                 select new TenantShortDto { CompleteAddress = a.CompleteAddress, Latitude = a.Latitude, Longitude = a.Logitude, AddressLine3 = a.AddressLine3, AddressLine2 = a.AddressLine2, AddressLine1 = a.AddressLine1, Country = a.Country, County = a.County, Town = a.Town, PostCode = a.PostCode, LogoUrl = o.LogoUrl, Name = o.Name, Id = o.Id }).ToList();
                return objResult;
            }
        }
        public List<TenantShortDto> GetByFilterByName(string name)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tenants                           
                                 join a in context.TenantAddress on o.AddressId equals a.Id                                
                                 where o.Publish == true && name.Contains(o.Name)
                                 orderby o.Name
                                 select new TenantShortDto { CompleteAddress = a.CompleteAddress, Latitude = a.Latitude, Longitude = a.Logitude, AddressLine3 = a.AddressLine3, AddressLine2 = a.AddressLine2, AddressLine1 = a.AddressLine1, Country = a.Country, County = a.County, Town = a.Town, PostCode = a.PostCode,LogoUrl = o.LogoUrl, Name = o.Name, Id = o.Id }).ToList();
                return objResult;
            }
        }
        
        public List<TenantShortDto> GetAll(int iskip, int itake)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tenants                             
                                 join a in context.TenantAddress on o.AddressId equals a.Id                               
                                 where o.Publish == true
                                 orderby o.Name
                                 select new TenantShortDto { CompleteAddress = a.CompleteAddress, Latitude = a.Latitude, Longitude = a.Logitude, AddressLine3 = a.AddressLine3, AddressLine2 = a.AddressLine2, AddressLine1 = a.AddressLine1, Country = a.Country, County = a.County, Town = a.Town, PostCode = a.PostCode, LogoUrl = o.LogoUrl, Name = o.Name, Id = o.Id }).Skip(iskip).Take(itake).ToList();
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
