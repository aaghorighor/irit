namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
    using System.Data.Linq;

    public class DeliveryAddress : IDeliveryAddress
    {
        public DeliveryAddressDto Get(Guid Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.DeliveryAddresses                                            
                                 where o.Id == Id
                                 select new DeliveryAddressDto { AddressLine = o.AddressLine, Distance = o.Distance, Duration = o.Duration, Latitude = o.Latitude, Logitude = o.Logitude, OrderId = o.OrderId, CreatedAt = o.CreatedAt, CreatedBy = o.CreatedBy, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }

        public DeliveryAddressDto GetByOrderId(Guid orderId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.DeliveryAddresses
                                 where o.OrderId == orderId
                                 select new DeliveryAddressDto { AddressLine = o.AddressLine, Distance = o.Distance, Duration = o.Duration, Latitude = o.Latitude, Logitude = o.Logitude, OrderId = o.OrderId, CreatedAt = o.CreatedAt, CreatedBy = o.CreatedBy, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }

        public bool Delete(Guid Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {               
                var objToDelete = context.DeliveryAddresses.SingleOrDefault(o => o.Id == Id);
                if (objToDelete != null)
                {
                    context.DeliveryAddresses.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }               
            }

            return response;
        }

        public Guid Insert(DeliveryAddressDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.DeliveryAddress() { AddressLine = entity.AddressLine, CreatedAt = entity.CreatedAt, Distance = entity.Distance, Duration = entity.Duration, Latitude = entity.Latitude, Logitude = entity.Logitude,  OrderId = entity.OrderId, CreatedBy = entity.CreatedBy, Id = entity.Id };
                context.DeliveryAddresses.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }

        public bool Update(DeliveryAddressDto entity)
        {
            bool response = false;

            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.DeliveryAddresses.SingleOrDefault(o => o.Id == entity.DeliveryId);

                if (objToUpdate != null)
                {                  
                    objToUpdate.AddressLine = entity.AddressLine;                  
                    objToUpdate.Distance = entity.Distance;
                    objToUpdate.Duration = entity.Duration;
                    objToUpdate.Latitude = entity.Latitude;
                    objToUpdate.Logitude = entity.Logitude;                             

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
    }
}


