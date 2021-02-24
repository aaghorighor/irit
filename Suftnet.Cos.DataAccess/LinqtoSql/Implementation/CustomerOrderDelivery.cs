namespace Suftnet.Cos.DataAccess
{
    using Suftnet.DataFactory.LinqToSql;
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

    public class CustomerOrderDelivery : ICustomerOrderDelivery
    {
        public CustomerOrderDeliveryDto Get(Guid orderId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from a in context.CustomerOrderDeliveries
                                 join r in context.CustomerOrders on a.CustomerIdOrderId equals r.Id
                                 join o in context.CustomerAddresses on a.AddressId equals o.Id
                                 where r.OrderId == orderId
                                 select new CustomerOrderDeliveryDto { AddressLine = o.AddressLine, Postcode = o.Postcode, CompleteAddress = o.CompleteAddress, Country = o.Country, County = o.County, Latitude = o.Latitude, Longitude = o.Longitude, Town = o.Town, Id = o.Id }).FirstOrDefault();
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

        public Guid Insert(CustomerOrderDeliveryDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.CustomerOrderDelivery() { Id = entity.Id, CustomerIdOrderId = entity.CustomerIdOrderId, AddressId = entity.AddressId, CreatedAt = entity.CreatedDT, CreatedBy = entity.CreatedBy };
                context.CustomerOrderDeliveries.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }             
       
    }
}


