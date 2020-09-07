namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;

    public interface IOrderPayment 
   {
       Guid Insert(OrderPaymentDto entity);
       List<PaymentDto> GetByOrderId(Guid orderId);
       Guid GetOrderIdByPaymentId(Guid paymentId);
       IEnumerable<PaymentDto> GetByUser(string userName);
       decimal GetTotalPaymentByOrderId(Guid orderId);
   }
}
