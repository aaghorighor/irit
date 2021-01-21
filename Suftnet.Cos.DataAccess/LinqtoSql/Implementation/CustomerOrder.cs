namespace Suftnet.Cos.DataAccess
{
    using Suftnet.DataFactory.LinqToSql;
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

    public class CustomerOrder : ICustomerOrder
    {
        public List<CustomerOrderDto> GetAll(Guid customerId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from a in context.CustomerOrders
                                 join o in context.Orders on a.OrderId equals o.Id
                                 join s in context.OrderStatuses on o.StatusId equals s.Id
                                 join r in context.OrderTypes on o.OrderTypeId equals r.Id
                                 join p in context.PaymentStatuses on o.PaymentStatusId equals p.Id
                                 where a.CustomerId == customerId
                                 orderby o.CreatedDt descending
                                 select new CustomerOrderDto { PaymentStatus = p.Name, UpdateDate = o.UpdateDt, UpdateBy = o.UpdateBy, OrderType = r.Name, Mobile = o.Mobile, FirstName = o.FirstName, LastName = o.LastName, ExpectedGuest = o.ExpectedGuest, Time = o.Time, Balance = o.Balance, Payment = o.Payment, TotalTax = o.TotalTax, StatusId = o.StatusId, TableId = o.TableId, Status = s.Name, GrandTotal = o.GrandTotal, OrderTypeId = o.OrderTypeId, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }

        public bool Delete(Guid Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {

                var objToDelete = context.CustomerOrders.SingleOrDefault(o => o.Id.Equals(Id));
                if (objToDelete != null)
                {
                    context.CustomerOrders.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }

            return response;
        }

        public Guid Insert(CustomerOrderDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.CustomerOrder() { Id = entity.Id, OrderId = entity.OrderId, CustomerId = entity.CustomerId, CreatedAt = entity.CreatedDT, CreatedBy = entity.CreatedBy };
                context.CustomerOrders.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }             
       
    }
}


