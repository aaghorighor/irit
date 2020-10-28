namespace Suftnet.Cos.DataAccess
{
    using Suftnet.DataFactory.LinqToSql;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Report : IReport
    {       
        private readonly IMenu _menu;        
     
        public Report(IMenu menu)
        {
            _menu = menu;          
        }

        public IEnumerable<OrderDto> GetSales(TermDto term)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 join ot in context.OrderTypes on o.OrderTypeId equals ot.Id                            
                                 where o.TenantId == term.TenantId && (o.CreatedDt >= term.StartDate && o.CreatedDt <= term.EndDate) && o.StatusId == term.StatusId
                                 orderby o.CreatedDt descending
                                 select new OrderDto { GrandTotal = o.GrandTotal, OrderType = ot.Name,  OrderTypeId = o.OrderTypeId, Total = o.Total,CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }
        public IEnumerable<OrderDto> GetSalesByUserName(TermDto term)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 join ot in context.OrderTypes on o.OrderTypeId equals ot.Id                               
                                 where (o.TenantId == term.TenantId && o.CreatedBy == term.UserName) && (o.CreatedDt >= term.StartDate && o.CreatedDt <= term.EndDate.Date) && o.StatusId == term.StatusId
                                 orderby o.CreatedDt descending
                                 select new OrderDto { GrandTotal = o.GrandTotal, OrderType = ot.Name, OrderTypeId = o.OrderTypeId, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }       
                                               
        public IEnumerable<PaymentDto> GetPaymentByDates(TermDto term) 
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Payments
                                 join m in context.OrderTypes on o.PaymentMethodId equals m.Id                          
                                 where  (o.CreatedDt >= term.StartDate && o.CreatedDt <= term.EndDate)
                                 orderby o.CreatedDt descending
                                 select new PaymentDto {  Reference = o.Reference, PaymentMethodId = o.PaymentMethodId, PaymentMethod = m.Name, Amount = o.Amount, CreatedDT = o.CreatedDt, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }
      
        public IEnumerable<PaymentDto> GetPaymentByDatesAndUsername(TermDto term)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Payments
                                 join m in context.OrderTypes on o.PaymentMethodId equals m.Id                                
                                 where (o.CreatedDt >= term.StartDate && o.CreatedDt <= term.EndDate) && o.CreatedBy.ToLower() == term.UserName.ToLower()
                                 orderby o.CreatedDt descending
                                 select new PaymentDto {Reference = o.Reference, PaymentMethodId = o.PaymentMethodId, PaymentMethod = m.Name, Amount = o.Amount, CreatedDT = o.CreatedDt, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }

        public IEnumerable<BestSellerDto>  GetBestSellers(TermDto term)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.OrderDetails
                                 join s in context.Menus on o.MenuId equals s.Id
                                 where (s.TenantId == term.TenantId) && (o.CreatedDt >= term.StartDate && o.CreatedDt <= term.EndDate)
                                 group o by s.Name into bestSellers
                                 orderby bestSellers.Key descending
                                 select new BestSellerDto { Count = bestSellers.Count(), ItemName = bestSellers.Key }).ToList();
                return objResult;
            }
        }
       
        public IEnumerable<OrderDto> GetOrders(TermDto term)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 join s in context.OrderStatuses on o.StatusId equals s.Id
                                 join q in context.OrderTypes on o.OrderTypeId equals q.Id
                                 join t in context.Tables on o.TableId equals t.Id
                                 where o.TenantId == term.TenantId && (o.CreatedDt >= term.StartDate && o.CreatedDt <= term.EndDate && o.OrderTypeId == term.OrderTypeId && o.StatusId == term.StatusId)
                                 orderby o.Id descending
                                 select new OrderDto { Table = t.Number, TotalDiscount = o.TotalDiscount, TotalTax = o.TotalTax, ExpectedGuest = o.ExpectedGuest,  OrderType = q.Name, Balance= o.Balance, Payment = o.Payment, StatusId = o.StatusId, Status = s.Name, GrandTotal = o.GrandTotal, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }

        public IEnumerable<OrderDto> GetReservationOrders(TermDto term)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 join s in context.OrderStatuses on o.StatusId equals s.Id
                                 join q in context.OrderTypes on o.OrderTypeId equals q.Id
                                 join t in context.Tables on o.TableId equals t.Id
                                 where o.TenantId == term.TenantId && (o.OrderTypeId == term.OrderTypeId && o.StatusId == term.StatusId)
                                 orderby o.Id descending
                                 select new OrderDto { LastName =o.LastName, FirstName = o.FirstName, Time = o.Time, Email = o.Email, Mobile = o.Mobile, Table = t.Number, TotalDiscount = o.TotalDiscount, TotalTax = o.TotalTax, ExpectedGuest = o.ExpectedGuest, OrderType = q.Name, Balance = o.Balance, Payment = o.Payment, StatusId = o.StatusId, Status = s.Name, GrandTotal = o.GrandTotal, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }            

        public IEnumerable<MenuDto> GetMenus(Guid tenantId)
        {
            return _menu.GetAll(tenantId);
        }         

    }
}
