namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
    using System.Data.Linq;

    public class Payment : IPayment
    {
        public PaymentDto Get(Guid Id)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Payments
                                 join m in context.PaymentMethods on o.PaymentMethodId equals m.Id
                                 join a in context.AccountTypes on o.AccountTypeId equals a.Id
                                 where o.Id == Id
                                 select new PaymentDto { AccountType = a.Name, AccountTypeId = o.AccountTypeId, UpdateDate = o.UpdateDt, UpdateBy = o.UpdateBy, Reference = o.Reference, PaymentMethodId = o.PaymentMethodId, PaymentMethod = m.Name, Amount = o.Amount, CreatedDT = o.CreatedDt, CreatedBy = o.CreatedBy, Id = o.Id }).FirstOrDefault();
                return objResult;
            }
        }

        public bool Delete(Guid Id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var _objToDelete = context.OrderPayments.Where(o => o.PaymentId == Id);
                if (_objToDelete != null)
                {
                    context.OrderPayments.RemoveRange(_objToDelete);
                    context.SaveChanges();                  
                }

                var objToDelete = context.Payments.SingleOrDefault(o => o.Id == Id);
                if (objToDelete != null)
                {
                    context.Payments.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }               
            }

            return response;
        }

        public Guid Insert(PaymentDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.Payment() { AccountTypeId = entity.AccountTypeId, Id = entity.Id, TenantId = entity.TenantId, Reference = entity.Reference, PaymentMethodId = entity.PaymentMethodId,  Amount = entity.Amount, CreatedDt = entity.CreatedDT, CreatedBy = entity.CreatedBy };
                context.Payments.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }

        public bool Update(PaymentDto entity)
        {
            bool response = false;

            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Payments.SingleOrDefault(o => o.Id == entity.Id);

                if (objToUpdate != null)
                {                  
                    objToUpdate.Amount = entity.Amount;                  
                    objToUpdate.PaymentMethodId = entity.PaymentMethodId;

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

        public List<PaymentDto> GetAll(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Payments
                                 join m in context.PaymentMethods on o.PaymentMethodId equals m.Id
                                 join a in context.AccountTypes on o.AccountTypeId equals a.Id
                                 where o.TenantId == tenantId
                                 orderby o.Id descending 
                                 select new PaymentDto { AccountType = a.Name, AccountTypeId = o.AccountTypeId, UpdateDate = o.UpdateDt, UpdateBy = o.UpdateBy, Reference = o.Reference, PaymentMethodId = o.PaymentMethodId, PaymentMethod = m.Name, Amount = o.Amount, CreatedDT = o.CreatedDt, CreatedBy = o.CreatedBy, Id = o.Id }).ToList();
                return objResult;
            }
        }       

        
        public List<PaymentDto> GetAll(Guid tenantId, int iskip, int itake, string isearch)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Payments
                                 join m in context.PaymentMethods on o.PaymentMethodId equals m.Id
                                 join a in context.AccountTypes on o.AccountTypeId equals a.Id
                                 where m.Name.Contains(isearch) || o.Reference.Contains(isearch) && o.TenantId == tenantId
                                 orderby o.Id descending 
                                 select new PaymentDto { AccountType = a.Name, AccountTypeId = o.AccountTypeId, UpdateDate = o.UpdateDt, UpdateBy = o.UpdateBy, Reference = o.Reference, PaymentMethodId = o.PaymentMethodId, PaymentMethod = m.Name, Amount = o.Amount, CreatedDT = o.CreatedDt, CreatedBy = o.CreatedBy, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                return objResult;
            }
        }

        public List<PaymentDto> GetAll(Guid tenantId,int iskip, int itake)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Payments
                                 join m in context.PaymentMethods on o.PaymentMethodId equals m.Id
                                 join a in context.AccountTypes on o.AccountTypeId equals a.Id
                                 where o.TenantId == tenantId
                                 orderby o.Id descending 
                                 select new PaymentDto { AccountType = a.Name, AccountTypeId = o.AccountTypeId, UpdateDate = o.UpdateDt, UpdateBy = o.UpdateBy, Reference = o.Reference, PaymentMethodId = o.PaymentMethodId, PaymentMethod = m.Name, Amount = o.Amount, CreatedDT = o.CreatedDt, CreatedBy = o.CreatedBy, Id = o.Id }).Skip(iskip).Take(itake).ToList();
                return objResult;
            }
        }

        public int GetByCurrentPayment(Guid tenantId,DateTime currentDate)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Payments
                                 where o.CreatedDt == currentDate && o.TenantId == tenantId
                                 select o).Count();
                return objResult;
            }
        }

        public int Count(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Payments
                                 where o.TenantId == tenantId
                                 select o).Count();
                return objResult;
            }
        }   
    }
}


