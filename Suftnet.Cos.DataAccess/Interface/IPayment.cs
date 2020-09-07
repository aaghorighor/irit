namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;

    public interface IPayment 
    {
        PaymentDto Get(Guid Id);
        bool Delete(Guid Id);
        Guid Insert(PaymentDto entity);
        bool Update(PaymentDto entity);
        List<PaymentDto> GetAll(Guid tenantId);
        List<PaymentDto> GetAll(Guid tenantId, int iskip, int itake, string isearch);
        int GetByCurrentPayment(Guid tenantId, DateTime currentDate);      
        List<PaymentDto> GetAll(Guid tenantId, int iskip, int itake);     
        int Count(Guid tenantId);
    }
}

