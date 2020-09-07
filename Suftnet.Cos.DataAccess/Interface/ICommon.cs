namespace Suftnet.Cos.DataAccess
{
   using System;
   using System.Collections.Generic;

    public interface ITenantCommon 
    {     
       List<TenantCommonDto> GetAll(int settingid, int tenantId);
       List<TenantCommonDto> GetById(int Id);
       List<TenantCommonDto> GetAll(int settingid, int tenantId, string externalId);
       bool Update(TenantCommonDto entity);
       int Insert(TenantCommonDto entity);
       bool Delete(int id);
       bool DeleteTenant(int tenantId);
       TenantCommonDto Get(int id);
    }
}
