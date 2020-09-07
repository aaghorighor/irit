namespace Suftnet.Cos.DataAccess
{
   using System;
   using System.Collections.Generic;

    public interface IDiscount
    {     
       List<DiscountDto> GetAll(Guid tenantId);
       List<MobileDiscountDto> Fetch(Guid tenantId);       
       bool Update(DiscountDto entity);
       Guid Insert(DiscountDto entity);
       bool Delete(Guid id);
       bool DeleteTenant(Guid tenantId);
       DiscountDto Get(Guid id);      
    }
}
