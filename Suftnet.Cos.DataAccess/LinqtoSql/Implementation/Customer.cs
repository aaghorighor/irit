namespace Suftnet.Cos.DataAccess
{
    using Suftnet.DataFactory.LinqToSql;
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

    public class Customer : ICustomer
    {
        public CustomerDto Get(Guid Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Customers                               
                                 where o.Id == Id
                                 select new CustomerDto { FirstName = o.FirstName, LastName = o.LastName, Email = o.Email, DeviceId = o.DeviceId, Mobile = o.Mobile, Serial=o.Serial, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }

        public bool Delete(Guid Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {

                var objToDelete = context.Customers.SingleOrDefault(o => o.Id.Equals(Id));
                if (objToDelete != null)
                {
                    context.Customers.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }

            return response;
        }

        public Guid Insert(CustomerDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.Customer() { Id = entity.Id, FirstName = entity.FirstName, LastName = entity.LastName, Email = entity.Email, DeviceId = entity.DeviceId, Serial = entity.Serial, Mobile = entity.Mobile, TenantId = entity.TenantId, UserId = entity.UserId, Active = entity.Active, CreatedAt = entity.CreatedDT, CreatedBy = entity.CreatedBy };
                context.Customers.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }

        public bool Update(UpadteCustomerDto entity)
        {
            bool response = false;

            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Customers.SingleOrDefault(o => o.Id == entity.Id && o.TenantId == entity.ExternalId);

                if (objToUpdate != null)
                {                                               
                    objToUpdate.FirstName = entity.FirstName;
                    objToUpdate.LastName = entity.LastName;              
                    objToUpdate.Mobile = entity.Mobile;

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

        public List<CustomerDto> GetAll(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Customers                            
                                 where o.TenantId == tenantId
                                 orderby o.Id descending
                                 select new CustomerDto { Active = o.Active, FirstName = o.FirstName, LastName = o.LastName, Email = o.Email, DeviceId = o.DeviceId, Mobile = o.Mobile, Serial = o.Serial, Id = o.Id }).ToList();                           
                return objResult;
            }
        }
       
    }
}


