namespace Suftnet.Cos.DataAccess
{
    using Suftnet.DataFactory.LinqToSql;
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

    public class CustomerNotification : ICustomerNotification
    {
        public List<CustomerOrderNotificationDto> GetAll(Guid customerId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from a in context.CustomerOrderNotifications                                 
                                 where a.CustomerId == customerId
                                 orderby a.CreatedAt
                                 select new CustomerOrderNotificationDto {  Messages =a.Messages, CreatedBy = a.CreatedBy, CreatedAt = a.CreatedAt, Id = a.Id }).ToList();
                return objResult;
            }
        }

        public bool Delete(Guid Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {

                var objToDelete = context.CustomerOrderNotifications.SingleOrDefault(o => o.Id.Equals(Id));
                if (objToDelete != null)
                {
                    context.CustomerOrderNotifications.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }

            return response;
        }

        public Guid Insert(CustomerOrderNotificationDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.CustomerOrderNotification() { Id = entity.Id, CustomerId = entity.CustomerId, Messages = entity.Messages, CreatedAt = entity.CreatedDT, CreatedBy = entity.CreatedBy };
                context.CustomerOrderNotifications.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }             
       
    }
}


