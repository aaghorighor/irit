namespace Suftnet.Cos.DataAccess
{
    using Suftnet.DataFactory.LinqToSql;
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

    public class DeliveryOrder : IDeliveryOrder
    {
        public List<MobileCustomerOrderDto> FetchBy(string userId, Guid statusId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from a in context.CustomerOrders
                                 join o in context.Orders on a.OrderId equals o.Id
                                 join c in context.Customers on a.CustomerId equals c.Id
                                 join i in context.DeliveryOrders on a.OrderId equals i.OrderId
                                 join e in context.CustomerOrderDeliveries on a.Id equals e.CustomerOrderId
                                 join d in context.CustomerAddresses on e.AddressId equals d.Id
                                 join s in context.OrderStatuses on o.StatusId equals s.Id                              
                                 where i.UserId == userId && o.StatusId != statusId
                                 orderby o.CreatedDt descending
                                 select new MobileCustomerOrderDto { OrderTypeId = o.OrderTypeId, DiscountRate = o.DiscountRate, CreatedAt = o.CreatedDt, OrderId = o.Id, StatusId = o.StatusId, CompletedAddress = d.CompleteAddress, AddressId = e.AddressId, CustomerId = a.CustomerId, Email = c.Email, Mobile = c.Mobile, FirstName = c.FirstName, LastName = c.LastName, Balance = o.Balance, Payment = o.Payment, TotalTax = o.TotalTax, TotalDiscount = o.TotalDiscount, DeliveryCost = o.DeliveryCost, TaxRate = o.TaxRate, Status = s.Name, GrandTotal = o.GrandTotal, Total = o.Total }).ToList();
                return objResult;
            }
        }

        public List<MobileCustomerOrderDto> FetchByDelivered(string userId, Guid statusId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from a in context.CustomerOrders
                                 join o in context.Orders on a.OrderId equals o.Id
                                 join c in context.Customers on a.CustomerId equals c.Id
                                 join i in context.DeliveryOrders on a.OrderId equals i.OrderId
                                 join e in context.CustomerOrderDeliveries on a.Id equals e.CustomerOrderId
                                 join d in context.CustomerAddresses on e.AddressId equals d.Id
                                 join s in context.OrderStatuses on o.StatusId equals s.Id
                                 where i.UserId == userId && o.StatusId == statusId
                                 orderby o.CreatedDt descending
                                 select new MobileCustomerOrderDto { OrderTypeId = o.OrderTypeId, DiscountRate = o.DiscountRate, CreatedAt = o.CreatedDt, OrderId = o.Id, StatusId = o.StatusId, CompletedAddress = d.CompleteAddress, AddressId = e.AddressId, CustomerId = a.CustomerId, Email = c.Email, Mobile = c.Mobile, FirstName = c.FirstName, LastName = c.LastName, Balance = o.Balance, Payment = o.Payment, TotalTax = o.TotalTax, TotalDiscount = o.TotalDiscount, DeliveryCost = o.DeliveryCost, TaxRate = o.TaxRate, Status = s.Name, GrandTotal = o.GrandTotal, Total = o.Total }).ToList();
                return objResult;
            }
        }

        public bool Delete(Guid Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.DeliveryOrders.SingleOrDefault(o => o.Id.Equals(Id));
                if (objToDelete != null)
                {
                    context.DeliveryOrders.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }

            return response;
        }

        public Guid Insert(DeliveryOrderDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.DeliveryOrder() { Id = entity.Id, OrderId = entity.OrderId, UserId = entity.UserId, CreatedAt = entity.CreatedAt, CreatedBy = entity.CreatedBy };
                context.DeliveryOrders.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }             
       
    }
}


