namespace Suftnet.Cos.DataAccess
{
    using Suftnet.DataFactory.LinqToSql;
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

    public class CustomerOrder : ICustomerOrder
    {        
        public List<CustomerOrderDto> Fetch(Guid tenantId, int iskip, int itake, string search)
        {
            if(string.IsNullOrEmpty(search))
            {
                using (var context = DataContextFactory.CreateContext())
                {
                    var objResult = (from a in context.CustomerOrders
                                     join o in context.Orders on a.OrderId equals o.Id
                                     join s in context.OrderStatuses on o.StatusId equals s.Id
                                     join p in context.PaymentStatuses on o.PaymentStatusId equals p.Id
                                     where o.TenantId == tenantId
                                     orderby o.CreatedDt descending
                                     select new CustomerOrderDto { OrderTypeId = o.OrderTypeId,  Email = o.Email, PaymentStatus = p.Name, UpdateDate = o.UpdateDt, UpdateBy = o.UpdateBy, Mobile = o.Mobile, FirstName = o.FirstName, LastName = o.LastName, Time = o.Time, Balance = o.Balance, Payment = o.Payment, TotalTax = o.TotalTax, TotalDiscount = o.TotalDiscount, DeliveryCost = o.DeliveryCost, TaxRate = o.TaxRate, StatusId = o.StatusId, Status = s.Name, GrandTotal = o.GrandTotal, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                    return objResult;
                }
            }else
            {
                return Fetch(tenantId, iskip, itake);
            }            
           
        }

        public List<CustomerOrderDto> Fetch(Guid tenantId, int iskip, int itake)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from a in context.CustomerOrders
                                 join o in context.Orders on a.OrderId equals o.Id
                                 join s in context.OrderStatuses on o.StatusId equals s.Id
                                 join p in context.PaymentStatuses on o.PaymentStatusId equals p.Id
                                 where o.TenantId == tenantId
                                 orderby o.CreatedDt descending
                                 select new CustomerOrderDto { OrderTypeId = o.OrderTypeId, Email = o.Email, PaymentStatus = p.Name, UpdateDate = o.UpdateDt, UpdateBy = o.UpdateBy, Mobile = o.Mobile, FirstName = o.FirstName, LastName = o.LastName, Time = o.Time, Balance = o.Balance, Payment = o.Payment, TotalTax = o.TotalTax, TotalDiscount = o.TotalDiscount, DeliveryCost = o.DeliveryCost, DiscountRate = o.DiscountRate, TaxRate = o.TaxRate, StatusId = o.StatusId, Status = s.Name, GrandTotal = o.GrandTotal, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                return objResult;
            }

        }

        public List<MobileCustomerOrderDto> Fetch(Guid customerId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from a in context.CustomerOrders
                                 join o in context.Orders on a.OrderId equals o.Id
                                 join c in context.Customers on a.CustomerId equals c.Id
                                 join e in context.CustomerOrderDeliveries on a.Id equals e.CustomerOrderId
                                 join d in context.CustomerAddresses on e.AddressId equals d.Id
                                 join s in context.OrderStatuses on o.StatusId equals s.Id
                                 join p in context.PaymentStatuses on o.PaymentStatusId equals p.Id
                                 where a.CustomerId == customerId
                                 orderby o.CreatedDt descending
                                 select new MobileCustomerOrderDto { CreatedAt = o.CreatedDt, OrderId = o.Id, StatusId = o.StatusId, CompletedAddress = d.CompleteAddress, AddressId = e.AddressId, CustomerId = customerId, Email = c.Email, PaymentStatus = p.Name, Mobile = c.Mobile, FirstName = c.FirstName, LastName = c.LastName, Balance = o.Balance, Payment = o.Payment, TotalTax = o.TotalTax, TotalDiscount = o.TotalDiscount, DeliveryCost = o.DeliveryCost, TaxRate = o.TaxRate, Status = s.Name, GrandTotal = o.GrandTotal, Total = o.Total }).ToList();
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


