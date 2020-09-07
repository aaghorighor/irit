namespace Suftnet.Cos.DataAccess
{
   using System;
   using System.Collections.Generic;

    public interface ITax
    {     
       List<TaxDto> GetAll(Guid tenantId);
       List<MobileTaxDto> Fetch(Guid tenantId);       
       bool Update(TaxDto entity);
       Guid Insert(TaxDto entity);
       bool Delete(Guid id);
       bool DeleteTenant(Guid tenantId);
       TaxDto Get(Guid id);      
    }
}
