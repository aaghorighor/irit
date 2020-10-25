namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
    using System.Data.Linq;
    using System.Globalization;

    public class OrderDetail : IOrderDetail
    {
        public OrderDetailDto Get(Guid Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.OrderDetails 
                                 join p in context.Menus on o.MenuId equals p.Id                                 
                                 where o.Id == Id
                                 select new OrderDetailDto { IsKitchen = o.IsKitchen, Discount = o.Discount, ItemName = o.ItemName, Title = p.Name, TaxRate = o.Tax, CategoryId = p.CategoryId, MenuId = o.MenuId, OrderId = o.OrderId, Total = o.LineTotal, Quantity = o.Quantity, Price = o.Price, CreatedBy = o.CreatedBy, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        } 
    
        public bool Delete(Guid Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.OrderDetails.SingleOrDefault(o => o.Id == Id);
                if (objToDelete != null)
                {
                    context.OrderDetails.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }

            return response;          
        }

        public void ClearOrderDetailByOrderId(Guid orderId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.OrderDetails.Where(o => o.OrderId == orderId);
                if (objToDelete != null)
                {
                    context.OrderDetails.RemoveRange(objToDelete);
                    context.SaveChanges();                    
                }
               
            }
        }

        public Guid Insert(OrderDetailDto entity)
        {          
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.OrderDetail() { DiscountRate = 0, TaxRate =0, Id = entity.Id, IsKitchen = entity.IsKitchen, ItemName = entity.ItemName, IsProcessed = entity.IsProcessed, Discount = entity.Discount, Tax = entity.TaxRate, MenuId = entity.MenuId, OrderId = entity.OrderId, LineTotal = entity.LineTotal, Quantity = entity.Quantity, Price = entity.Price, CreatedDt = entity.CreatedDT, CreatedBy = entity.CreatedBy };
                context.OrderDetails.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }          
        }

        public bool Update(OrderDetailDto entity)
        {
            bool response = false;
            
                using (var context = DataContextFactory.CreateContext())
                {
                    var objToUpdate = context.OrderDetails.SingleOrDefault(o => o.Id == entity.Id);

                    if (objToUpdate != null)
                    {
                            objToUpdate.OrderId = entity.OrderId;                      
                            objToUpdate.Price = entity.Price;                                                 
                            objToUpdate.Quantity = entity.Quantity;
                            objToUpdate.MenuId = entity.MenuId;

                            objToUpdate.UpdateDt = entity.UpdateDate;
                            objToUpdate.UpdateBy = entity.UpdateBy;

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

        public bool UpdateCompletedOrders(Guid orderId)
        {
            bool response = false;

            using (var context = DataContextFactory.CreateContext())
            {
                var items = (from o in context.OrderDetails where o.OrderId == orderId && o.IsProcessed == false select o).ToList();

                foreach (var item in items)
                {
                    var objToUpdate = context.OrderDetails.SingleOrDefault(o => o.Id == item.Id);

                    if (objToUpdate != null)
                    {
                        objToUpdate.IsKitchen = true;      
                        
                        objToUpdate.UpdateBy = Environment.UserName;
                        objToUpdate.UpdateDt = DateTime.UtcNow;

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
                }                

                return response;
            }
        }
        public List<OrderDetailDto> GetAll(Guid orderId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.OrderDetails
                                 join p in context.Menus on o.MenuId equals p.Id 
                                 join u in context.Units on p.UnitId equals u.Id                                
                                 where o.OrderId == orderId
                                 orderby o.Id descending 
                                 select new OrderDetailDto { IsKitchen = o.IsKitchen, Discount = o.Discount, IsProcessed= o.IsProcessed, ItemName = o.ItemName, TaxRate = o.Tax, MenuId = o.MenuId, OrderId = o.OrderId, Title = p.Name, Unit = u.Name, Total = o.LineTotal, Quantity = o.Quantity, Price = o.Price, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }

        public List<OrderDetailWrapperDto> GetByTableOrders(Guid statusId, Guid secondarystatusId, Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from s in context.Orders
                                 join t in context.Tables on s.TableId equals t.Id
                                 join r in context.OrderTypes on s.OrderTypeId equals r.Id
                                 let orderDetail = (from o in context.OrderDetails
                                                     join p in context.Menus on o.MenuId equals p.Id
                                                     join u in context.Units on p.UnitId equals u.Id
                                                     where o.OrderId == s.Id && p.IsKitchen == true && o.IsProcessed == false
                                                     select new OrderDetailDto { Discount = o.Discount, IsProcessed = o.IsProcessed, ItemName = o.ItemName, TaxRate = o.Tax, MenuId = o.MenuId, OrderId = o.OrderId, Title = p.Name, Unit = u.Name, Total = o.LineTotal, Quantity = o.Quantity, Price = o.Price, CreatedBy = o.CreatedBy, Id = o.Id }).ToList()
                                 where (s.StatusId == statusId || s.StatusId == secondarystatusId ) && orderDetail.Count > 0 && s.TenantId == tenantId
                                 orderby s.CreatedDt ascending 
                                 select new OrderDetailWrapperDto { OrderTypeId = s.OrderTypeId, OrderType = r.Name, Table = t.Number, OrderId = s.Id, OrderDetail = orderDetail }).ToList();

                return objResult;
            }
        }      
      
    }
}
