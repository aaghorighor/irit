namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;

    public interface ITenant
    {
        TenantDto Get(Guid Id);
        TenantDto IsValid(string customerStripId);
        bool IsTenantNew(Guid tenantId);
        bool Delete(Guid Id);
        Guid Insert(TenantDto entity);
        bool AdminUpdate(TenantDto entity);
        bool UpdateStatus(Guid tenantId, bool isExpired);
        bool UpdateStartUp(Guid tenantId, bool status);
        bool Update(TenantDto entity);
        bool CancelTrial(RequestDto entity);      
        List<TenantShortDto> GetAll(int iskip, int itake, bool status);     
        bool UpdateCustomer(TenantDto tenant);
        int Count(bool status);
        int Status(Guid statusId, Guid tenantId);
        TenantDto Expired(Guid tenantId);
        int Count();
        TenantAdapter GetAll(int iskip, int itake, string terms);
        TenantAdapter GetAll(int iskip, int itake);

    }
}
