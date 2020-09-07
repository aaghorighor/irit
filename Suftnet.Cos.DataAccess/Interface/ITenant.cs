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
        List<TenantDto> GetAll();
        List<TenantDto> GetAll(int statusId);
        List<TenantDto> Startup(int statusId);
        List<TenantShortDto> GetByFilterByPostCode(string postcode);
        List<TenantShortDto> GetByFilterByCoordinate(string latitude, string longitude);
        List<TenantShortDto> GetByFilterByAddress(string completeAddress);
        List<TenantShortDto> GetByFilterByName(string name);
        List<TenantShortDto> GetAll(int iskip, int itake);
        int Count();
        bool UpdateCustomer(TenantDto tenant);
       
    }
}
