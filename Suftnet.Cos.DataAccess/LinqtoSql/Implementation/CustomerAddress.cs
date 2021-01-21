namespace Suftnet.Cos.DataAccess
{
    using Suftnet.DataFactory.LinqToSql;
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

    public class CustomerAddress : ICustomerAddress
    {
        public CustomerAddressDto Get(Guid Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.CustomerAddresses                               
                                 where o.Id == Id
                                 select new CustomerAddressDto { AddressLine = o.AddressLine, AddressLine2 = o.AddressLine2, AddressLine3 = o.AddressLine3, CompleteAddress = o.CompleteAddress, Country = o.Country, County = o.County, Latitude = o.Latitude, Logitude = o.Logitude, Town = o.Town, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }

        public bool Delete(Guid Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.CustomerAddresses.SingleOrDefault(o => o.Id.Equals(Id));
                if (objToDelete != null)
                {
                    context.CustomerAddresses.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }

            return response;
        }

        public Guid Insert(CustomerAddressDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.CustomerAddress() { Town = entity.Town, Id = entity.Id, AddressLine = entity.AddressLine, AddressLine2 = entity.AddressLine2, AddressLine3 = entity.AddressLine3, CompleteAddress = entity.CompleteAddress, Country = entity.Country, County = entity.County, CustomerId = entity.CustomerId, Latitude = entity.Latitude, Logitude = entity.Logitude, CreatedAt = entity.CreatedDT, CreatedBy = entity.CreatedBy };
                context.CustomerAddresses.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }

        public bool Update(CustomerAddressDto entity)
        {
            bool response = false;

            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.CustomerAddresses.SingleOrDefault(o => o.Id == entity.Id);

                if (objToUpdate != null)
                {
                    objToUpdate.Town = entity.Town;                                   
                    objToUpdate.AddressLine = entity.AddressLine;
                    objToUpdate.AddressLine2 = entity.AddressLine2;
                    objToUpdate.AddressLine3 = entity.AddressLine3;
                    objToUpdate.Country = entity.Country;
                    objToUpdate.Town = entity.Town;
                    objToUpdate.County = entity.County;
                    objToUpdate.Latitude = entity.Latitude;
                    objToUpdate.Logitude = entity.Logitude;
                    objToUpdate.CompleteAddress = entity.CompleteAddress;                 

                    try
                    {
                        context.SaveChanges();
                        response = true;
                    }
                    catch (ChangeConflictException)
                    {
                        response = false;
                    }
                }

                return response;
            }
        }        

        public List<CustomerAddressDto> GetAll(Guid customerId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.CustomerAddresses
                                 where o.CustomerId == customerId
                                 orderby o.Id descending
                                 select new CustomerAddressDto { AddressLine = o.AddressLine, AddressLine2 = o.AddressLine2, AddressLine3 = o.AddressLine3, CompleteAddress = o.CompleteAddress, Country = o.Country, County = o.County, Latitude = o.Latitude, Logitude = o.Logitude, Town = o.Town, Id = o.Id }).ToList();                     
                return objResult;
            }
        }
       
    }
}


