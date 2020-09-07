﻿namespace Suftnet.Cos.DataAccess
{
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
    using System;

    public class Order : IOrder
    {       
        public OrderDto Get(Guid Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 join s in context.OrderStatuses on o.StatusId equals s.Id
                                 join t in context.Tables on o.TableId equals t.Id                                
                                 join r in context.OrderTypes on o.OrderTypeId equals r.Id
                                 where o.Id == Id 
                                 select new OrderDto { OrderType = r.Name, Mobile = o.Mobile, FirstName = o.FirstName, LastName = o.LastName, ExpectedGuest = o.ExpectedGuest, Time = o.Time, Balance = o.Balance, Payment = o.Payment, StatusId = o.StatusId, TableId = o.TableId, Status = s.Name, Table = t.Number , TotalTax = o.TotalTax, TotalDiscount = o.TotalDiscount, GrandTotal = o.GrandTotal, OrderTypeId = o.OrderTypeId, Total = o.Total, CreatedDT = o.CreatedDt, CreatedBy = o.CreatedBy, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }       
        public bool Delete(Guid orderId)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.Orders.SingleOrDefault(o => o.Id == orderId);
                if (objToDelete != null)
                {
                    context.Orders.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }

            return response;          
        }
        public Guid Insert(OrderDto entity)
        {          
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.Order() { Note = entity.Note, UpdateBy = entity.UpdateBy, UpdateDt = entity.UpdateDate, Email = entity.Email, GrandTotal = 0, Total = 0, TotalDiscount = 0, TotalTax = 0, StartDt = entity.StartDt, TenantId = entity.TenantId, Mobile = entity.Mobile, FirstName = entity.FirstName, LastName = entity.LastName, Balance = entity.Balance, ExpectedGuest = entity.ExpectedGuest, Time = entity.Time, Payment = 0, TableId = entity.TableId, StatusId = entity.StatusId, CreatedDt = entity.CreatedDT, OrderTypeId = entity.OrderTypeId, CreatedBy = entity.CreatedBy, Id = entity.Id };
                context.Orders.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }          
        }
        public bool UpdateSalesOrder(OrderDto entity)
        {
            bool response = false;

            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Orders.SingleOrDefault(o => o.Id == entity.Id);

                if (objToUpdate != null)
                {
                    objToUpdate.GrandTotal = entity.GrandTotal;
                    objToUpdate.Balance = entity.Balance;
                    objToUpdate.Payment = entity.Payment;                  
                    objToUpdate.Total = entity.Total;
                    objToUpdate.TotalTax = entity.TotalTax;                   
                    objToUpdate.TotalDiscount = entity.TotalDiscount;                    
                    objToUpdate.StatusId = entity.StatusId;

                    objToUpdate.UpdateBy = entity.UpdateBy;
                    objToUpdate.UpdateDt = entity.UpdateDate;

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
        public bool Update(OrderDto entity)
        {
            bool response = false;
            
                using (var context = DataContextFactory.CreateContext())
                {
                    var objToUpdate = context.Orders.SingleOrDefault(o => o.Id == entity.Id);

                    if (objToUpdate != null)
                    {
                            objToUpdate.TableId = entity.TableId;                                  
                            objToUpdate.Time = entity.Time;
                            objToUpdate.ExpectedGuest = entity.ExpectedGuest;                        
                            objToUpdate.FirstName = entity.FirstName;
                            objToUpdate.LastName = entity.LastName;                    
                            objToUpdate.StatusId = entity.StatusId;

                            objToUpdate.UpdateBy = entity.UpdateBy;
                            objToUpdate.UpdateDt = entity.UpdateDate;

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
        public bool UpdateReserve(OrderDto entity)
        {
            bool response = false;

            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Orders.SingleOrDefault(o => o.Id == entity.Id);

                if (objToUpdate != null)
                {
                    objToUpdate.TableId = entity.TableId;
                    objToUpdate.Time = entity.Time;
                    objToUpdate.ExpectedGuest = entity.ExpectedGuest;               
                    objToUpdate.FirstName = entity.FirstName;
                    objToUpdate.LastName = entity.LastName;
                    objToUpdate.Mobile = entity.Mobile;
                    objToUpdate.StatusId = entity.StatusId;
                    objToUpdate.Email = entity.Email;
                    objToUpdate.Note = entity.Note;

                    objToUpdate.UpdateBy = entity.UpdateBy;
                    objToUpdate.UpdateDt = entity.UpdateDate;

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

        public List<OrderDto> GetAll(Guid orderTypeId, Guid tenantId, int iskip, int itake, string isearch)
        {
            if(!string.IsNullOrEmpty(isearch))
            {
                using (var context = DataContextFactory.CreateContext())
                {
                    var objResult = (from o in context.Orders
                                     join s in context.OrderStatuses on o.StatusId equals s.Id
                                     join t in context.Tables on o.TableId equals t.Id
                                     join r in context.OrderTypes on o.OrderTypeId equals r.Id
                                     where (o.TenantId == tenantId && o.OrderTypeId == orderTypeId) && (t.Number.Contains(isearch) || o.FirstName.Contains(isearch) || s.Name.Contains(isearch) || o.CreatedDt.ToString().Contains(isearch) || r.Name.Contains(isearch))
                                     orderby o.Id descending
                                     select new OrderDto { UpdateDate = o.UpdateDt, UpdateBy = o.UpdateBy, OrderType = r.Name, Mobile = o.Mobile, FirstName = o.FirstName, LastName = o.LastName, ExpectedGuest = o.ExpectedGuest, Time = o.Time, Balance = o.Balance, Payment = o.Payment, TotalTax = o.TotalTax, StatusId = o.StatusId, TableId = o.TableId, Status = s.Name, Table = t.Number, GrandTotal = o.GrandTotal, OrderTypeId = o.OrderTypeId, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                    return objResult;
                }
            }else
            {
                return GetAll(orderTypeId, tenantId, iskip, itake);
            }
            
        }

        public List<OrderDto> GetAll(Guid orderTypeId, Guid tenantId, int iskip, int itake)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 join s in context.OrderStatuses on o.StatusId equals s.Id
                                 join t in context.Tables on o.TableId equals t.Id
                                 join r in context.OrderTypes on o.OrderTypeId equals r.Id
                                 where (o.TenantId == tenantId && o.OrderTypeId == orderTypeId)
                                 orderby o.CreatedDt descending
                                 select new OrderDto { UpdateDate = o.UpdateDt, UpdateBy = o.UpdateBy, OrderType = r.Name, Mobile = o.Mobile, FirstName = o.FirstName, LastName = o.LastName, ExpectedGuest = o.ExpectedGuest, Time = o.Time, Balance = o.Balance, Payment = o.Payment, TotalTax = o.TotalTax, StatusId = o.StatusId, TableId = o.TableId, Status = s.Name, Table = t.Number, GrandTotal = o.GrandTotal, OrderTypeId = o.OrderTypeId, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                return objResult;
            }
        }     
      
        public List<OrderDto> GetDeliveryOrders(Guid orderTypeId, Guid tenantId, int iskip, int itake, string isearch)
        {
            if (!string.IsNullOrEmpty(isearch))
            {
                using (var context = DataContextFactory.CreateContext())
                {
                    var objResult = (from o in context.Orders
                                     join s in context.OrderStatuses on o.StatusId equals s.Id
                                     join t in context.Tables on o.TableId equals t.Id
                                     join r in context.OrderTypes on o.OrderTypeId equals r.Id
                                     where o.TenantId == tenantId && (o.OrderTypeId == orderTypeId)
                                     orderby o.CreatedDt descending
                                     select new OrderDto { UpdateDate = o.UpdateDt, UpdateBy = o.UpdateBy, OrderType = r.Name, Mobile = o.Mobile, FirstName = o.FirstName, LastName = o.LastName, Time = o.Time, Balance = o.Balance, Payment = o.Payment, TotalTax = o.TotalTax, StatusId = o.StatusId, TableId = o.TableId, Status = s.Name, Table = t.Number, GrandTotal = o.GrandTotal, OrderTypeId = o.OrderTypeId, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                    return objResult;
                }
            }else
            {
                return GetDeliveryOrders(orderTypeId, tenantId, iskip, itake);
            }
        }

        public List<OrderDto> GetDeliveryOrders(Guid orderTypeId, Guid tenantId, int iskip, int itake)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 join s in context.OrderStatuses on o.StatusId equals s.Id
                                 join t in context.Tables on o.TableId equals t.Id
                                 join r in context.OrderTypes on o.OrderTypeId equals r.Id
                                 where o.TenantId == tenantId && (o.OrderTypeId == orderTypeId)
                                 orderby o.CreatedDt descending
                                 select new OrderDto { UpdateDate = o.UpdateDt, UpdateBy = o.UpdateBy, OrderType = r.Name, Mobile = o.Mobile, FirstName = o.FirstName, LastName = o.LastName, Time = o.Time, Balance = o.Balance, Payment = o.Payment, TotalTax = o.TotalTax, StatusId = o.StatusId, TableId = o.TableId, Status = s.Name, Table = t.Number, GrandTotal = o.GrandTotal, OrderTypeId = o.OrderTypeId, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                return objResult;
            }
        }

        public List<OrderDto> GetReserveOrders(Guid orderTypeId, Guid statusId, Guid tenantId, int iskip, int itake, string isearch)
        {
            if (!string.IsNullOrEmpty(isearch))
            {
                using (var context = DataContextFactory.CreateContext())
                {
                    var objResult = (from o in context.Orders
                                     join s in context.OrderStatuses on o.StatusId equals s.Id
                                     join t in context.Tables on o.TableId equals t.Id
                                     join r in context.OrderTypes on o.OrderTypeId equals r.Id
                                     where o.TenantId == tenantId && (o.OrderTypeId == orderTypeId)
                                     orderby o.CreatedDt descending
                                     select new OrderDto { Email = o.Email, Note = o.Note, StartDt = o.StartDt, UpdateDate = o.UpdateDt, UpdateBy = o.UpdateBy, OrderType = r.Name, Mobile = o.Mobile, FirstName = o.FirstName, LastName = o.LastName, ExpectedGuest = o.ExpectedGuest, Time = o.Time, Balance = o.Balance, Payment = o.Payment, TotalTax = o.TotalTax, StatusId = o.StatusId, TableId = o.TableId, Status = s.Name, Table = t.Number, GrandTotal = o.GrandTotal, OrderTypeId = o.OrderTypeId, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                    return objResult;
                }
            }
            else
            {
                return GetReserveOrders(orderTypeId, statusId, tenantId, iskip, itake);
            }
        }

        public List<OrderDto> GetReserveOrders(Guid orderTypeId, Guid statusId, Guid tenantId, int iskip, int itake)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 join s in context.OrderStatuses on o.StatusId equals s.Id
                                 join t in context.Tables on o.TableId equals t.Id
                                 join r in context.OrderTypes on o.OrderTypeId equals r.Id
                                 where o.TenantId == tenantId && (o.OrderTypeId == orderTypeId)
                                 orderby o.CreatedDt descending
                                 select new OrderDto { Email = o.Email, Note = o.Note, StartDt = o.StartDt, UpdateDate = o.UpdateDt, UpdateBy = o.UpdateBy, OrderType = r.Name, Mobile = o.Mobile, FirstName = o.FirstName, LastName = o.LastName, ExpectedGuest = o.ExpectedGuest, Time = o.Time, Balance = o.Balance, Payment = o.Payment, TotalTax = o.TotalTax, StatusId = o.StatusId, TableId = o.TableId, Status = s.Name, Table = t.Number, GrandTotal = o.GrandTotal, OrderTypeId = o.OrderTypeId, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                return objResult;
            }
        }

        public List<OrderDto> GetByOrderType(Guid tenantId, Guid orderTypeId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 join s in context.OrderStatuses on o.StatusId equals s.Id
                                 join t in context.Tables on o.TableId equals t.Id
                                 join r in context.OrderTypes on o.OrderTypeId equals r.Id
                                 where (o.TenantId == tenantId && o.OrderTypeId == orderTypeId)
                                 orderby o.Id descending
                                 select new OrderDto { UpdateDate = o.UpdateDt, UpdateBy = o.UpdateBy, OrderType = r.Name, Mobile = o.Mobile, FirstName = o.FirstName, LastName = o.LastName, ExpectedGuest = o.ExpectedGuest, Time = o.Time, Balance = o.Balance, Payment = o.Payment, TotalTax = o.TotalTax, StatusId = o.StatusId, TableId = o.TableId, Status = s.Name, Table = t.Number, GrandTotal = o.GrandTotal, OrderTypeId = o.OrderTypeId, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }
        public List<OrderDto> Filter(Guid tenantId, DateTime filterDate, Guid orderTypeId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 join s in context.OrderStatuses on o.StatusId equals s.Id
                                 join t in context.Tables on o.TableId equals t.Id
                                 join r in context.OrderTypes on o.OrderTypeId equals r.Id
                                 where o.TenantId == tenantId && (o.OrderTypeId == orderTypeId && o.CreatedDt >= filterDate)
                                 orderby o.Id descending
                                 select new OrderDto { UpdateDate = o.UpdateDt, UpdateBy = o.UpdateBy, OrderType = r.Name, Mobile = o.Mobile, FirstName = o.FirstName, LastName = o.LastName, ExpectedGuest = o.ExpectedGuest, Time = o.Time, Balance = o.Balance, Payment = o.Payment, TotalTax = o.TotalTax, StatusId = o.StatusId, TableId = o.TableId, Status = s.Name, Table = t.Number, GrandTotal = o.GrandTotal, OrderTypeId = o.OrderTypeId, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }
        public List<OrderDto> Filter(Guid tenantId, DateTime filterDate)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 join s in context.OrderStatuses on o.StatusId equals s.Id
                                 join t in context.Tables on o.TableId equals t.Id
                                 join r in context.OrderTypes on o.OrderTypeId equals r.Id
                                 where (o.TenantId == tenantId && o.CreatedDt.Date >= filterDate.Date)
                                 orderby o.Id descending
                                 select new OrderDto { UpdateDate = o.UpdateDt, UpdateBy = o.UpdateBy, OrderType = r.Name, Mobile = o.Mobile, FirstName = o.FirstName, LastName = o.LastName, ExpectedGuest = o.ExpectedGuest, Time = o.Time, Balance = o.Balance, Payment = o.Payment, TotalTax = o.TotalTax, StatusId = o.StatusId, TableId = o.TableId, Status = s.Name, Table = t.Number, GrandTotal = o.GrandTotal, OrderTypeId = o.OrderTypeId, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }
        public IEnumerable<OrderDto> GetAllOrderByStatus(TermDto term)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 join s in context.OrderStatuses on o.StatusId equals s.Id
                                 where o.TenantId == term.TenantId && ((o.CreatedDt >= term.StartDate && o.CreatedDt <= term.EndDate) && (o.OrderTypeId == term.OrderTypeId && o.StatusId == term.StatusId))
                                 orderby o.Id descending
                                 select new OrderDto { Balance = o.Balance, Payment = o.Payment, StatusId = o.StatusId, Status = s.Name, GrandTotal = o.GrandTotal, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }
        public IEnumerable<OrderDto> GetAllSales(TermDto term)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 join s in context.OrderStatuses on o.StatusId equals s.Id
                                 where o.TenantId == term.TenantId && (o.CreatedDt >= term.StartDate && o.CreatedDt <= term.EndDate)
                                 orderby o.Id descending
                                 select new OrderDto { Balance = o.Balance, Payment = o.Payment, StatusId = o.StatusId, Status = s.Name, GrandTotal = o.GrandTotal, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }
        public IEnumerable<OrderDto> GetAllSalesByUserName(TermDto term)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 join s in context.OrderStatuses on o.StatusId equals s.Id
                                 where o.TenantId == term.TenantId && ((o.CreatedDt >= term.StartDate.Date && o.CreatedDt <= term.EndDate.Date) && o.CreatedBy == term.UserName)
                                 orderby o.Id descending
                                 select new OrderDto { Balance = o.Balance, Payment = o.Payment, StatusId = o.StatusId, Status = s.Name, GrandTotal = o.GrandTotal, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }
        public bool UpdateOrderStatus(Guid tenantId, Guid orderId, Guid statusId, Guid orderTypeId, DateTime createDt, string createdBy)
        {
            bool response = false;

            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Orders.SingleOrDefault(o => o.Id == orderId && o.TenantId == tenantId);

                if (objToUpdate != null)
                {
                    objToUpdate.StatusId = statusId;
                    objToUpdate.OrderTypeId = orderTypeId;

                    objToUpdate.UpdateBy = createdBy;
                    objToUpdate.UpdateDt = createDt;

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
        public bool UpdateOrderStatus(Guid orderId, Guid statusId, DateTime createDt, string createdBy, Guid tenantId)
        {
            bool response = false;

            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Orders.SingleOrDefault(o => o.Id == orderId && o.TenantId == tenantId);

                if (objToUpdate != null)
                {
                    objToUpdate.StatusId = statusId;

                    objToUpdate.UpdateBy = createdBy;
                    objToUpdate.UpdateDt = createDt;

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
        public int Count(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 where o.TenantId == tenantId
                                 select o
                                 ).Count();
                return objResult;
            }
        }   
        public int Count(Guid statusId, Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 where o.TenantId == tenantId && o.StatusId == statusId 
                                 select o
                                 ).Count();
                return objResult;
            }
        }
        public int Count(Guid statusId, Guid tenantId, Guid orderTypeId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 where o.TenantId == tenantId && o.StatusId == statusId && o.OrderTypeId == orderTypeId
                                 select o
                                 ).Count();
                return objResult;
            }
        }
    }
}