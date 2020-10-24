namespace Suftnet.Cos.DataAccess
{
    using System.Collections.Generic;
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
    using System.Data.Linq;
    using System;

    public class OrderPayment : IOrderPayment
    {                  
        public bool Delete(Guid id)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.OrderPayments.SingleOrDefault(o => o.OrderId == id);
                if (objToDelete != null)
                {
                    context.OrderPayments.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
               
            }

            return response;          
        }

        public Guid Insert(OrderPaymentDto entity)
        {          
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.OrderPayment() { Id = entity.Id, CreatedBy = entity.CreatedBy, CreatedDt = entity.CreatedDT, PaymentId = entity.PaymentId, OrderId = entity.OrderId };
                context.OrderPayments.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }          
        }

        public List<PaymentDto> GetByOrderId(Guid orderId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.OrderPayments
                                 join b in context.Payments on o.PaymentId equals b.Id
                                 join m in context.PaymentMethods on b.PaymentMethodId equals m.Id
                                 where o.OrderId == orderId
                                 select new PaymentDto { PaymentMethod = m.Name,  CreatedDT = o.CreatedDt, CreatedBy = o.CreatedBy, AccountId = 0, Amount = b.Amount, PaymentMethodId = b.PaymentMethodId, Id = o.Id }).ToList();
                return objResult;
            }
        }

        public IEnumerable<PaymentDto> GetByUser(string userName)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.OrderPayments
                                 join b in context.Payments on o.PaymentId equals b.Id
                                 join m in context.PaymentMethods on b.PaymentMethodId equals m.Id
                                 where o.CreatedBy == userName
                                 select new PaymentDto { PaymentMethod = m.Name, CreatedBy = o.CreatedBy,  AccountId = 0, Amount = b.Amount, PaymentMethodId = b.PaymentMethodId, Id = o.Id }).ToList();
                return objResult;
            }
        }

        public Guid GetOrderIdByPaymentId(Guid paymentId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.OrderPayments
                                 where o.PaymentId == paymentId
                                 select o.OrderId).FirstOrDefault();
                return objResult;
            }
        }

        public decimal GetTotalPaymentByOrderId(Guid orderId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = (from o in context.OrderPayments
                                 join b in context.Payments on o.PaymentId equals b.Id                                
                                 where o.OrderId == orderId
                                 select b).ToList();
                return obj.Sum(x => x.Amount);
            }
        }
    }
}
