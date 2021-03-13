namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
    using System.Data.Linq;

    public class OrderDetail : IOrderDetail
    {
        public OrderDetailDto Get(Guid Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.OrderDetails 
                                 join p in context.Menus on o.MenuId equals p.Id                                 
                                 where o.Id == Id
                                 select new OrderDetailDto { AddonIds = o.AddonIds, AddonItems = o.AddonItems, IsKitchen = o.IsKitchen, ItemName = o.ItemName, Title = p.Name, CategoryId = p.CategoryId, MenuId = o.MenuId, OrderId = o.OrderId, Total = o.LineTotal, Quantity = o.Quantity, Price = o.Price, CreatedBy = o.CreatedBy, Id = o.Id }).FirstOrDefault();
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
                var obj = new Action.OrderDetail() { AddonIds = entity.AddonIds, AddonItems = entity.AddonItems, Id = entity.Id, IsKitchen = entity.IsKitchen, ItemName = entity.ItemName, IsProcessed = entity.IsProcessed, MenuId = entity.MenuId, OrderId = entity.OrderId, LineTotal = entity.LineTotal, Quantity = entity.Quantity, Price = entity.Price, CreatedDt = entity.CreatedDT, CreatedBy = entity.CreatedBy };
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
        public bool UpdateCompletedOrders(Guid orderId, string userName)
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
                        objToUpdate.IsKitchen = false;
                        objToUpdate.IsProcessed = true;

                        objToUpdate.UpdateBy = userName;
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
                                 orderby o.CreatedDt descending
                                 select new OrderDetailDto { AddonIds = o.AddonIds, AddonItems = o.AddonItems, IsKitchen = o.IsKitchen,IsProcessed= o.IsProcessed, ItemName = o.ItemName, MenuId = o.MenuId, OrderId = o.OrderId, Title = p.Name, Unit = u.Name, Total = o.LineTotal, Quantity = o.Quantity, Price = o.Price, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }
        public IList<BasketDto> FetchBasket(Guid orderId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from x in context.OrderDetails
                                 where x.OrderId == orderId
                                 orderby x.CreatedDt descending
                                 select new BasketDto { OrderId = orderId, AddonIds = x.AddonIds, Addons = x.AddonItems, IsProcessed = x.IsProcessed, Menu = x.ItemName, MenuId = x.MenuId, Price = x.Price }).ToList();
               return objResult;
            }
        }
        public IList<MobileBasketDto> FetchMobileBasket(Guid orderId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from x in context.OrderDetails
                                 where x.OrderId == orderId
                                 orderby x.CreatedDt descending
                                 select new MobileBasketDto { Quantity = x.Quantity, AddonIds = x.AddonIds, addonNames = x.AddonItems, Menu = x.ItemName, Price = x.Price }).ToList();
                return objResult;
            }
        }
        public List<OrderDetailWrapperDto> FetchPendingOrders(Guid statusId, Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from s in context.Orders
                                 join t in context.Tables on s.TableId equals t.Id
                                 join r in context.OrderTypes on s.OrderTypeId equals r.Id
                                 let kitchenBaskets = (from o in context.OrderDetails
                                                     join p in context.Menus on o.MenuId equals p.Id                                                  
                                                     where o.OrderId == s.Id && p.IsKitchen == true && o.IsProcessed == false
                                                     select new OrderDetailDto { CreatedDT = o.CreatedDt, AddonIds = o.AddonIds, AddonItems = o.AddonItems, IsProcessed = o.IsProcessed, ItemName = o.ItemName,  MenuId = o.MenuId, OrderId = o.OrderId, Title = p.Name, Total = o.LineTotal, Quantity = o.Quantity, Price = o.Price, CreatedBy = o.CreatedBy, Id = o.Id }).ToList()
                                 where kitchenBaskets.Count > 0 && s.TenantId == tenantId 
                                 orderby s.CreatedDt ascending 
                                 select new OrderDetailWrapperDto { OrderTypeId = s.OrderTypeId, OrderType = r.Name, Table = t.Number, OrderId = s.Id, OrderDetail = kitchenBaskets }).ToList();

                return objResult;
            }
        }
        public List<KitchenAdapter> FetchKitchenOrders(Guid statusId, Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from s in context.Orders
                                 join t in context.Tables on s.TableId equals t.Id
                                 join r in context.OrderTypes on s.OrderTypeId equals r.Id
                                 let kitchenBaskets = (from o in context.OrderDetails
                                                    where o.OrderId == s.Id && o.IsKitchen == true && o.IsProcessed == false
                                                    select new KitchenBasketDto { Quantity = o.Quantity, AddonItems = o.AddonItems, IsProcessed = o.IsProcessed, ItemName = o.ItemName, Id = o.Id }).ToList()
                                 where kitchenBaskets.Count > 0 && s.TenantId == tenantId 
                                 orderby s.CreatedDt ascending
                                 select new KitchenAdapter { OrderTypeId = s.OrderTypeId, Note = s.Note, CreatedDT =s.CreatedDt, OrderType = r.Name, Table = t.Number, OrderId = s.Id, KitchenBasket = kitchenBaskets }).ToList();

                return objResult;
            }
        }

        public List<KitchenAdapter> FetchKitchenDeliveryOrders(Guid statusId, Guid tenantId, Guid orderTypeId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from s in context.Orders                                
                                 join r in context.OrderTypes on s.OrderTypeId equals r.Id
                                 let kitchenBaskets = (from o in context.OrderDetails
                                                      where o.OrderId == s.Id && o.IsKitchen == true && o.IsProcessed == false
                                                      select new KitchenBasketDto { Quantity = o.Quantity, AddonItems = o.AddonItems, IsProcessed = o.IsProcessed, ItemName = o.ItemName, Id = o.Id }).ToList()
                                 where kitchenBaskets.Count > 0 && s.TenantId == tenantId && s.OrderTypeId == orderTypeId
                                 orderby s.CreatedDt ascending
                                 select new KitchenAdapter { OrderTypeId = s.OrderTypeId, Note = s.Note, CreatedDT = s.CreatedDt, OrderType = r.Name, OrderId = s.Id, KitchenBasket = kitchenBaskets }).ToList();

                return objResult;
            }
        }

    }
}
