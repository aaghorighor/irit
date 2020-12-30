namespace Suftnet.Cos.DataAccess
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
        public OrderDto FetchDeliveryOrder(Guid orderId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 join s in context.OrderStatuses on o.StatusId equals s.Id                             
                                 join r in context.OrderTypes on o.OrderTypeId equals r.Id
                                 where o.Id == orderId
                                 select new OrderDto { DeliveryCost = o.DeliveryCost, OrderType = r.Name, Mobile = o.Mobile, FirstName = o.FirstName, LastName = o.LastName, ExpectedGuest = o.ExpectedGuest, Time = o.Time, Balance = o.Balance, Payment = o.Payment, StatusId = o.StatusId, TableId = o.TableId, Status = s.Name, TotalTax = o.TotalTax, TotalDiscount = o.TotalDiscount, GrandTotal = o.GrandTotal, OrderTypeId = o.OrderTypeId, Total = o.Total, CreatedDT = o.CreatedDt, CreatedBy = o.CreatedBy, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }
        public List<OrderDto> FetchDeliveryByStatus(Guid tenantId, Guid statusId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 join s in context.OrderStatuses on o.StatusId equals s.Id
                                 join r in context.OrderTypes on o.OrderTypeId equals r.Id
                                 where o.Id == tenantId && o.StatusId == statusId
                                 orderby o.CreatedDt descending
                                 select new OrderDto { DeliveryCost = o.DeliveryCost, OrderType = r.Name, Mobile = o.Mobile, FirstName = o.FirstName, LastName = o.LastName, ExpectedGuest = o.ExpectedGuest, Time = o.Time, Balance = o.Balance, Payment = o.Payment, StatusId = o.StatusId, TableId = o.TableId, Status = s.Name, TotalTax = o.TotalTax, TotalDiscount = o.TotalDiscount, GrandTotal = o.GrandTotal, OrderTypeId = o.OrderTypeId, Total = o.Total, CreatedDT = o.CreatedDt, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }
        public CartOrderDto FetchOrder(Guid orderId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 let baskets = (from x in context.OrderDetails                                                                                                 
                                                    where x.OrderId == o.Id 
                                                    select new BasketDto { ItemName = x.ItemName, OrderId = o.Id, AddonIds = x.AddonIds, Addons = x.AddonItems, IsProcessed = x.IsProcessed, Menu = x.ItemName, MenuId = x.MenuId, Price = x.Price}).ToList()
                                 let itemOrder = new ItemOrderDto { Tax = o.TaxRate, Discount = o.DiscountRate, TableFor = o.ExpectedGuest, TableId = o.TableId, Balance = o.Balance, Paid = o.Payment, TotalTax = o.TotalTax, TotalDiscount = o.TotalDiscount, GrandTotal = o.GrandTotal,Total = o.Total, ExternalId = o.Id }                               
                                 where o.Id == orderId
                                 select new CartOrderDto { ExternalId = orderId, UserName = "Not Set", Baskets = baskets, Order = itemOrder }).FirstOrDefault();
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
                    var objToDeliveryAddresses = context.DeliveryAddresses.SingleOrDefault(o => o.Id == orderId);

                    if(objToDeliveryAddresses != null)
                    {
                        context.DeliveryAddresses.Remove(objToDeliveryAddresses);
                        context.SaveChanges();
                    }

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
                var obj = new Action.Order() { TableNumber = entity.TableNumber, PaymentStatusId= entity.PaymentStatusId, DeliveryCost = entity.DeliveryCost, DiscountRate = 0,  TaxRate = 0, Note = entity.Note, UpdateBy = entity.UpdateBy, UpdateDt = entity.UpdateDate, Email = entity.Email, GrandTotal = 0, Total = 0, TotalDiscount = 0, TotalTax = 0, StartDt = entity.StartDt, TenantId = entity.TenantId, Mobile = entity.Mobile, FirstName = entity.FirstName, LastName = entity.LastName, Balance = entity.Balance, ExpectedGuest = entity.ExpectedGuest, Time = entity.Time, Payment = 0, TableId = entity.TableId, StatusId = entity.StatusId, CreatedDt = entity.CreatedDT, OrderTypeId = entity.OrderTypeId, CreatedBy = entity.CreatedBy, Id = entity.Id };
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
                    objToUpdate.TaxRate = entity.TaxRate;
                    objToUpdate.DiscountRate = entity.DiscountRate;
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
        public bool UpdatePayment(OrderDto entity)
        {
            bool response = false;

            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Orders.SingleOrDefault(o => o.Id == entity.Id);

                if (objToUpdate != null)
                {
                    objToUpdate.Payment = entity.Payment;
                    objToUpdate.Balance = entity.Balance;                   
                    objToUpdate.PaymentStatusId = entity.PaymentStatusId;
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
        public bool UpdateDelivery(OrderDto entity)
        {
            bool response = false;

            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Orders.SingleOrDefault(o => o.Id == entity.Id);

                if (objToUpdate != null)
                {                 
                    objToUpdate.Time = entity.Time;
                    objToUpdate.Mobile = entity.Mobile;
                    objToUpdate.Email = entity.Email;
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
                    objToUpdate.OrderTypeId = entity.OrderTypeId;
                    objToUpdate.TableNumber = entity.TableNumber;

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
                                     join p in context.PaymentStatuses on o.PaymentStatusId equals p.Id
                                     join t in context.Tables on o.TableId equals t.Id
                                     join r in context.OrderTypes on o.OrderTypeId equals r.Id
                                     where (o.TenantId == tenantId && o.OrderTypeId == orderTypeId) && (t.Number.Contains(isearch) || o.FirstName.Contains(isearch) || s.Name.Contains(isearch) || o.CreatedDt.ToString().Contains(isearch) || r.Name.Contains(isearch))
                                     orderby o.Id descending
                                     select new OrderDto { PaymentStatus = p.Name, UpdateDate = o.UpdateDt, UpdateBy = o.UpdateBy, OrderType = r.Name, Mobile = o.Mobile, FirstName = o.FirstName, LastName = o.LastName, ExpectedGuest = o.ExpectedGuest, Time = o.Time, Balance = o.Balance, Payment = o.Payment, TotalTax = o.TotalTax, StatusId = o.StatusId, TableId = o.TableId, Status = s.Name, Table = t.Number, GrandTotal = o.GrandTotal, OrderTypeId = o.OrderTypeId, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).Skip(iskip).Take(itake).ToList();
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
                                 join p in context.PaymentStatuses on o.PaymentStatusId equals p.Id
                                 join t in context.Tables on o.TableId equals t.Id
                                 join r in context.OrderTypes on o.OrderTypeId equals r.Id
                                 where (o.TenantId == tenantId && o.OrderTypeId == orderTypeId)
                                 orderby o.CreatedDt descending
                                 select new OrderDto { PaymentStatus = p.Name, UpdateDate = o.UpdateDt, UpdateBy = o.UpdateBy, OrderType = r.Name, Mobile = o.Mobile, FirstName = o.FirstName, LastName = o.LastName, ExpectedGuest = o.ExpectedGuest, Time = o.Time, Balance = o.Balance, Payment = o.Payment, TotalTax = o.TotalTax, StatusId = o.StatusId, TableId = o.TableId, Status = s.Name, Table = t.Number, GrandTotal = o.GrandTotal, OrderTypeId = o.OrderTypeId, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                return objResult;
            }
        }           
        public List<DeliveryAddressDto> GetDeliveryOrders(Guid orderTypeId, Guid tenantId, int iskip, int itake, string isearch)
        {
            if (!string.IsNullOrEmpty(isearch))
            {
                using (var context = DataContextFactory.CreateContext())
                {
                    var objResult = (from o in context.DeliveryAddresses.Include("Order")
                                     join s in context.OrderStatuses on o.Order.StatusId equals s.Id                                  
                                     join r in context.OrderTypes on o.Order.OrderTypeId equals r.Id
                                     let order = o.Order
                                     where (o.Order.TenantId == tenantId && o.Order.OrderTypeId == orderTypeId) && (order.Mobile.Contains(isearch) || order.Email.Contains(isearch) || order.FirstName.Contains(isearch) || order.LastName.Contains(isearch) || s.Name.Contains(isearch) || r.Name.Contains(isearch) || o.AddressLine.Contains(isearch))
                                     orderby o.Order.CreatedDt descending
                                     select new DeliveryAddressDto { DeliveryCost = order.DeliveryCost, DeliveryId = o.Id, Email = order.Email, AddressLine = o.AddressLine, Distance = o.Distance, Duration = o.Duration, Latitude = o.Latitude, Logitude = o.Logitude, UpdateDate = order.UpdateDt, UpdateBy = order.UpdateBy, OrderType = r.Name, Mobile = order.Mobile, FirstName = order.FirstName, LastName = order.LastName, Time = order.Time, Balance = order.Balance, Payment = order.Payment, TotalTax = order.TotalTax, StatusId = order.StatusId, TableId = order.TableId, Status = s.Name, GrandTotal = order.GrandTotal,  OrderTypeId = order.OrderTypeId, Total = order.Total, CreatedBy = o.CreatedBy, Id = order.Id }).Skip(iskip).Take(itake).ToList();
                    return objResult;
                }
            }else
            {
                return GetDeliveryOrders(orderTypeId, tenantId, iskip, itake);
            }
        }
        public List<DeliveryAddressDto> GetDeliveryOrders(Guid orderTypeId, Guid tenantId, int iskip, int itake)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.DeliveryAddresses.Include("Order")
                                 join s in context.OrderStatuses on o.Order.StatusId equals s.Id
                                 join r in context.OrderTypes on o.Order.OrderTypeId equals r.Id
                                 let order = o.Order
                                 where o.Order.TenantId == tenantId && (o.Order.OrderTypeId == orderTypeId)
                                 orderby o.Order.CreatedDt descending
                                 select new DeliveryAddressDto { DeliveryCost = order.DeliveryCost, DeliveryId = o.Id, Email = order.Email, AddressLine = o.AddressLine, Distance = o.Distance, Duration = o.Duration, Latitude = o.Latitude, Logitude = o.Logitude, UpdateDate = order.UpdateDt, UpdateBy = order.UpdateBy, OrderType = r.Name, Mobile = order.Mobile, FirstName = order.FirstName, LastName = order.LastName, Time = order.Time, Balance = order.Balance, Payment = order.Payment, TotalTax = order.TotalTax, StatusId = order.StatusId, TableId = order.TableId, Status = s.Name, GrandTotal = order.GrandTotal, OrderTypeId = order.OrderTypeId, Total = order.Total, CreatedBy = o.CreatedBy, Id = order.Id }).Skip(iskip).Take(itake).ToList();
                return objResult;
            }
        }
        public List<OrderDto> GetReserveOrders(Guid orderTypeId, Guid tenantId, int iskip, int itake, string isearch)
        {
            if (!string.IsNullOrEmpty(isearch))
            {
                using (var context = DataContextFactory.CreateContext())
                {
                    var objResult = (from o in context.Orders
                                     join s in context.OrderStatuses on o.StatusId equals s.Id                                   
                                     join r in context.OrderTypes on o.OrderTypeId equals r.Id
                                     where (o.TenantId == tenantId && o.OrderTypeId == orderTypeId) && (o.Mobile.Contains(isearch) || o.Email.Contains(isearch) || o.FirstName.Contains(isearch) || o.LastName.Contains(isearch) || s.Name.Contains(isearch) || r.Name.Contains(isearch))
                                     orderby o.CreatedDt descending
                                     select new OrderDto { TableNumber = o.TableNumber, Email = o.Email, Note = o.Note, StartDt = o.StartDt, UpdateDate = o.UpdateDt, UpdateBy = o.UpdateBy, OrderType = r.Name, Mobile = o.Mobile, FirstName = o.FirstName, LastName = o.LastName, ExpectedGuest = o.ExpectedGuest, Time = o.Time, Balance = o.Balance, Payment = o.Payment, TotalTax = o.TotalTax, StatusId = o.StatusId, TableId = o.TableId, Status = s.Name, GrandTotal = o.GrandTotal, OrderTypeId = o.OrderTypeId, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                    return objResult;
                }
            }
            else
            {
                return GetReserveOrders(orderTypeId, tenantId, iskip, itake);
            }
        }
        public List<OrderDto> GetReserveOrders(Guid orderTypeId, Guid tenantId, int iskip, int itake)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 join s in context.OrderStatuses on o.StatusId equals s.Id                            
                                 join r in context.OrderTypes on o.OrderTypeId equals r.Id
                                 where o.TenantId == tenantId && (o.OrderTypeId.Equals(orderTypeId))
                                 orderby o.CreatedDt descending
                                 select new OrderDto { TableNumber = o.TableNumber, Email = o.Email, Note = o.Note, StartDt = o.StartDt, UpdateDate = o.UpdateDt, UpdateBy = o.UpdateBy, OrderType = r.Name, Mobile = o.Mobile, FirstName = o.FirstName, LastName = o.LastName, ExpectedGuest = o.ExpectedGuest, Time = o.Time, Balance = o.Balance, Payment = o.Payment, TotalTax = o.TotalTax, StatusId = o.StatusId, TableId = o.TableId, Status = s.Name, GrandTotal = o.GrandTotal, OrderTypeId = o.OrderTypeId, Total = o.Total, CreatedBy = o.CreatedBy, Id = o.Id }).Skip(iskip).Take(itake).ToList();
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
        public bool CancelOrder(Guid orderId, Guid orderStatusId, Guid paymentStatusId, DateTime createDt, string createdBy, Guid tenantId)
        {
            bool response = false;

            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Orders.SingleOrDefault(o => o.Id == orderId && o.TenantId == tenantId);

                if (objToUpdate != null)
                {
                    objToUpdate.StatusId = orderStatusId;
                    objToUpdate.PaymentStatusId = paymentStatusId;

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
        public int Count(Guid tenantId, Guid orderTypeId, params Guid[] statuses)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 where o.TenantId == tenantId && statuses.Contains(o.StatusId) && o.OrderTypeId == orderTypeId
                                 select o
                                 ).Count();
                return objResult;
            }
        }
        public int CountByOrderType(Guid tenantId, Guid orderTypeId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 where o.TenantId == tenantId && o.OrderTypeId.Equals(orderTypeId)
                                 select o
                                 ).Count();
                return objResult;
            }
        }
        public int CountByOrderType(Guid orderTypeId, Guid tenantId, Guid paymentStatusId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Orders
                                 where o.TenantId == tenantId && o.PaymentStatusId == paymentStatusId && o.OrderTypeId == orderTypeId
                                 select o
                                 ).Count();
                return objResult;
            }
        }
    }
}
