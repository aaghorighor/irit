namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
   
    public class TenantAddress : ITenantAddress
    {
        public TenantAddressDto Get(Guid Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.TenantAddress                                
                                 where o.Id == Id
                                 select new TenantAddressDto { Latitude = o.Latitude, Longitude = o.Logitude, Town = o.Town, CompleteAddress = o.CompleteAddress, County = o.County, AddressLine1 = o.AddressLine1, AddressLine2 = o.AddressLine2, AddressLine3 = o.AddressLine3, Country = o.Country, PostCode = o.PostCode, Id = o.Id , Active =(bool)o.Active }).FirstOrDefault();
                return objResult;
            }
        }

        public TenantAddressDto Get(Suftnet.Cos.DataAccess.Action.TenantAddress o)
        {
           return new TenantAddressDto { Latitude = o.Latitude, Longitude = o.Logitude, Town = o.Town, CompleteAddress = o.CompleteAddress, County = o.County, AddressLine1 = o.AddressLine1, AddressLine2 = o.AddressLine2, AddressLine3 = o.AddressLine3, Country = o.Country, PostCode = o.PostCode, Id = o.Id, Active = (bool)o.Active };
        }

        public bool Delete(Guid Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.TenantAddress.FirstOrDefault(o => o.Id == Id);
                if (objToDelete != null)
                {
                    context.TenantAddress.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }
            return response;
        }        

        public Guid Insert(TenantAddressDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Suftnet.Cos.DataAccess.Action.TenantAddress() { Id = entity.Id, Town = entity.Town, CompleteAddress = entity.CompleteAddress, County = entity.County, Latitude = entity.Latitude, Logitude = entity.Longitude, Active = entity.Active, AddressLine1 = entity.AddressLine1, AddressLine2 = entity.AddressLine2, AddressLine3 = entity.AddressLine3, Country = entity.Country, PostCode = entity.PostCode, CreatedBy = entity.CreatedBy, CreatedDT = entity.CreatedDT };
                context.TenantAddress.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }

        public bool UpdateByAddressId(TenantAddressDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.TenantAddress.SingleOrDefault(o => o.Id == entity.AddressId);
                if (objToUpdate != null)
                {
                    objToUpdate.Active = entity.Active;
                    objToUpdate.AddressLine1 = entity.AddressLine1;
                    objToUpdate.AddressLine2 = entity.AddressLine2;
                    objToUpdate.AddressLine3 = entity.AddressLine3;
                    objToUpdate.PostCode = entity.PostCode;
                    objToUpdate.Country = entity.Country;
                    objToUpdate.County = entity.County;
                    objToUpdate.Town = entity.Town;
                    objToUpdate.CompleteAddress = entity.CompleteAddress;
                    objToUpdate.Logitude = entity.Longitude;
                    objToUpdate.Latitude = entity.Latitude;

                    context.SaveChanges();
                    response = true;
                }
            }
            return response;
        }
        public bool Update(TenantAddressDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.TenantAddress.SingleOrDefault(o => o.Id == entity.Id);
                if (objToUpdate != null)
                {
                    objToUpdate.Active = entity.Active;
                    objToUpdate.AddressLine1 = entity.AddressLine1;
                    objToUpdate.AddressLine2 = entity.AddressLine2;
                    objToUpdate.AddressLine3 = entity.AddressLine3;
                    objToUpdate.PostCode = entity.PostCode;
                    objToUpdate.Country = entity.Country;
                    objToUpdate.County = entity.County;
                    objToUpdate.Town = entity.Town;
                    objToUpdate.CompleteAddress = entity.CompleteAddress;
                    objToUpdate.Logitude = entity.Longitude;
                    objToUpdate.Latitude = entity.Latitude;

                    context.SaveChanges();
                    response = true;
                }
            }
            return response;
        }               
    }
}
