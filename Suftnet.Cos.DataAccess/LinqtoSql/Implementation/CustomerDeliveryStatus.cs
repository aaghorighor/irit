namespace Suftnet.Cos.DataAccess
{
    using Suftnet.DataFactory.LinqToSql;
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

    public class CustomerDeliveryStatus : ICustomerDeliveryStatus
    {       
        public bool Delete(Guid Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {

                var objToDelete = context.CustomerDeliveryStatuses.SingleOrDefault(o => o.Id.Equals(Id));
                if (objToDelete != null)
                {
                    context.CustomerDeliveryStatuses.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }

            return response;
        }

        public Guid Insert(CustomerDeliveryStatusDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.CustomerDeliveryStatus() { Id = entity.Id, OrderId= entity.OrderId, OrderStatusId = entity.OrderStatusId, CreatedAt = entity.CreatedDT, CreatedBy = entity.CreatedBy };
                context.CustomerDeliveryStatuses.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }     

        public List<CustomerDeliveryStatusDto> GetAll(Guid orderId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.CustomerDeliveryStatuses
                                 join s in context.OrderStatuses on o.OrderStatusId equals s.Id
                                 where o.OrderId == orderId
                                 orderby o.CreatedAt descending
                                 select new CustomerDeliveryStatusDto { Status = s.Name, OrderId = o.OrderId, OrderStatusId = o.OrderStatusId, Id = o.Id }).ToList();                     
                return objResult;
            }
        }
       
    }
}


