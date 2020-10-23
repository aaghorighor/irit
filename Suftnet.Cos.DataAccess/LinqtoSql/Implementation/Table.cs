namespace Suftnet.Cos.DataAccess
{
    using Suftnet.DataFactory.LinqToSql;
    using System;
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

    public class Table : ITable
    {       
        public TableDto Get(Guid Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tables                                                      
                                 where o.Id == Id 
                                 select new TableDto { Number = o.Number, Size = o.Size, Active = o.Active, CreatedBy = o.CreatedBy, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }

        public bool Delete(Guid Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.Tables.SingleOrDefault(o => o.Id == Id);
                if (objToDelete != null)
                {
                    context.Tables.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }              
            }

            return response;
        }

        public Guid Insert(TableDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.Table() { Id = entity.Id, Active = entity.Active, Size = entity.Size, Number = entity.Number, TenantId = entity.TenantId, CreatedDt = entity.CreatedDT, CreatedBy = entity.CreatedBy };
                context.Tables.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }

        public bool Update(TableDto entity)
        {
            bool response = false;

            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Tables.SingleOrDefault(o => o.Id == entity.Id);

                if (objToUpdate != null)
                {                                                  
                    objToUpdate.Active = entity.Active;
                    objToUpdate.Size = entity.Size;
                    objToUpdate.Number = entity.Number;

                    if(entity.IsReset)
                    {
                        objToUpdate.OrderId = null;
                    }

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

        public bool UpdateStatus(Guid statusId, Guid tableId, Guid orderId, DateTime updatedDt,string updateBy)
        {
            bool response = false;

            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Tables.SingleOrDefault(o => o.Id == tableId);

                if (objToUpdate != null)
                {                     
                    objToUpdate.OrderId = orderId;
                    objToUpdate.UpdateBy = updateBy;
                    objToUpdate.UpdateDt = updatedDt;

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
        public List<TableDto> GetByStatus(bool status, Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tables
                                 where o.TenantId == tenantId && o.Active == status
                                 orderby o.Id descending
                                 select new TableDto { Active = o.Active, Number = o.Number, Size = o.Size, OrderId = o.OrderId, TimeIn = o.TimeIn, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }
        public List<TableDto> GetAll(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tables
                                 where o.TenantId == tenantId
                                 orderby o.Id descending
                                 select new TableDto { CreatedDT = o.CreatedDt, Active = o.Active, Number = o.Number, Size = o.Size, OrderId = o.OrderId, TimeIn = o.TimeIn, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }
        public int GetFreeCount(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tables
                                 where o.OrderId == null && o.Active == true   && o.TenantId == tenantId
                                 select o).Count();
                return objResult;
            }
        }
        public int GetOccupiedCount(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Tables
                                 where o.OrderId != null && o.Active == true && o.TenantId == tenantId
                                 select o).Count();
                return objResult;
            }
        }
       
    }
}


