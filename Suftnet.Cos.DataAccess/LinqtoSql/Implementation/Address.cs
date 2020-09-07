namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
   
    public class Address : IAddress
    {
        public AddressDto Get(int Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Addresses                                
                                 where o.Id == Id
                                 select new AddressDto { Latitude = o.Latitude, Longitude = o.Logitude, Town = o.Town, CompleteAddress = o.CompleteAddress, County = o.County, AddressLine1 = o.AddressLine1, AddressLine2 = o.AddressLine2, AddressLine3 = o.AddressLine3, Country = o.Country, PostCode = o.PostCode, Id = o.Id , Active =(bool)o.Active }).FirstOrDefault();
                return objResult;
            }
        }

        public AddressDto Get(Suftnet.Cos.DataAccess.Action.Address o)
        {
           return new AddressDto { Latitude = o.Latitude, Longitude = o.Logitude, Town = o.Town, CompleteAddress = o.CompleteAddress, County = o.County, AddressLine1 = o.AddressLine1, AddressLine2 = o.AddressLine2, AddressLine3 = o.AddressLine3, Country = o.Country, PostCode = o.PostCode, Id = o.Id, Active = (bool)o.Active };
        }

        public bool Delete(int Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.Addresses.FirstOrDefault(o => o.Id == Id);
                if (objToDelete != null)
                {
                    context.Addresses.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }
            return response;
        }        

        public int Insert(AddressDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Suftnet.Cos.DataAccess.Action.Address() { Town = entity.Town, CompleteAddress = entity.CompleteAddress, County = entity.County, Latitude = entity.Latitude, Logitude = entity.Longitude, Active = entity.Active, AddressLine1 = entity.AddressLine1, AddressLine2 = entity.AddressLine2, AddressLine3 = entity.AddressLine3, Country = entity.Country, PostCode = entity.PostCode, CreatedBy = entity.CreatedBy, CreatedDT = entity.CreatedDT };
                context.Addresses.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }

        public bool UpdateByAddressId(AddressDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Addresses.SingleOrDefault(o => o.Id == entity.AddressId);
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
        public bool Update(AddressDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Addresses.SingleOrDefault(o => o.Id == entity.Id);
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
