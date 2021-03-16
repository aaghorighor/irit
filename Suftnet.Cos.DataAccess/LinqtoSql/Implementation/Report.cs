﻿namespace Suftnet.Cos.DataAccess
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
                                               
        public IEnumerable<PaymentDto> GetPaymentByDates(TermDto term) 
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Payments
                                 join m in context.PaymentMethods on o.PaymentMethodId equals m.Id
                                 join a in context.AccountTypes on o.AccountTypeId equals a.Id
                                 where  (o.CreatedDt >= term.StartDate && o.CreatedDt <= term.EndDate) && (o.TenantId == term.TenantId)
                                 orderby o.CreatedDt descending
                                 select new PaymentDto { AccountType = a.Name, AccountTypeId = o.AccountTypeId, Reference = o.Reference, PaymentMethodId = o.PaymentMethodId, PaymentMethod = m.Name, Amount = o.Amount, CreatedDT = o.CreatedDt, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }

        public IEnumerable<MobilePaymentDto> FetchPayments(Guid externalId, int  take)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Payments
                                 join m in context.PaymentMethods on o.PaymentMethodId equals m.Id
                                 join a in context.AccountTypes on o.AccountTypeId equals a.Id
                                 where o.TenantId == externalId
                                 orderby o.CreatedDt descending
                                 select new MobilePaymentDto { AccountType = a.Name, AccountTypeId = o.AccountTypeId, OrderId = o.Reference, PaymentMethod = m.Name, Amount = o.Amount, CreatedDT = o.CreatedDt }).Take(take).ToList();
                return objResult;
            }
        }

        public IEnumerable<MobilePaymentDto> FetchPaymentByDates(DateQueryDto dateQueryDto)
        {
            if(string.IsNullOrEmpty(dateQueryDto.AccountTypeId))
            {
                using (var context = DataContextFactory.CreateContext())
                {
                    var objResult = (from o in context.Payments
                                     join m in context.PaymentMethods on o.PaymentMethodId equals m.Id
                                     join a in context.AccountTypes on o.AccountTypeId equals a.Id
                                     where (o.CreatedDt >= dateQueryDto.StartDate && o.CreatedDt <= dateQueryDto.EndDate) && (o.TenantId == dateQueryDto.ExernalId)
                                     orderby o.CreatedDt descending
                                     select new MobilePaymentDto { AccountType = a.Name, AccountTypeId = o.AccountTypeId, OrderId = o.Reference, PaymentMethod = m.Name, Amount = o.Amount, CreatedDT = o.CreatedDt }).ToList();
                    return objResult;
                }
            }else
            {
                return FetchPaymentByAccountType(dateQueryDto);
            }
            
        }

        public IEnumerable<MobilePaymentDto> FetchPaymentByAccountType(DateQueryDto dateQueryDto)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Payments
                                 join m in context.PaymentMethods on o.PaymentMethodId equals m.Id
                                 join a in context.AccountTypes on o.AccountTypeId equals a.Id
                                 where (o.CreatedDt >= dateQueryDto.StartDate && o.CreatedDt <= dateQueryDto.EndDate) && (o.TenantId == dateQueryDto.ExernalId && o.AccountTypeId == new Guid(dateQueryDto.AccountTypeId))
                                 orderby o.CreatedDt descending
                                 select new MobilePaymentDto { AccountType = a.Name, AccountTypeId = o.AccountTypeId, OrderId = o.Reference, PaymentMethod = m.Name, Amount = o.Amount, CreatedDT = o.CreatedDt }).ToList();
                return objResult;
            }
        }

        public IEnumerable<PaymentDto> GetPaymentByDatesAndUsername(TermDto term)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Payments
                                 join m in context.PaymentMethods on o.PaymentMethodId equals m.Id
                                 join a in context.AccountTypes on o.AccountTypeId equals a.Id
                                 where (o.CreatedDt >= term.StartDate && o.CreatedDt <= term.EndDate) && o.CreatedBy.ToLower() == term.UserName.ToLower()
                                 orderby o.CreatedDt descending
                                 select new PaymentDto { AccountType = a.Name, AccountTypeId = o.AccountTypeId, Reference = o.Reference, PaymentMethodId = o.PaymentMethodId, PaymentMethod = m.Name, Amount = o.Amount, CreatedDT = o.CreatedDt, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
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
