namespace Suftnet.Cos.DataAccess
{
   using System;
   using System.Collections.Generic;

    public interface IUnit
    {     
       List<UnitDto> GetAll(Guid tenantId);
       List<MobileUnitDto> Fetch(Guid tenantId);       
       bool Update(UnitDto entity);
       Guid Insert(UnitDto entity);
       bool Delete(Guid id);
       bool DeleteTenant(Guid tenantId);
        UnitDto Get(Guid id);      
    }
}
