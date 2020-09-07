namespace Suftnet.Cos.DataAccess
{
   using System;
   using System.Collections.Generic;

    public interface IAddOnType
    {     
       List<AddonTypeDto> GetAll(Guid tenantId);
       List<MobileAddonTypeDto> Fetch(Guid tenantId);       
       bool Update(AddonTypeDto entity);
       Guid Insert(AddonTypeDto entity);
       bool Delete(Guid id);
       bool DeleteTenant(Guid tenantId);
       AddonTypeDto Get(Guid id);      
    }
}
