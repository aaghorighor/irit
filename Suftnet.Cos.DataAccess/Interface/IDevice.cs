namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
 
   public interface IDevice
   {      
       List<DeviceDto> GetAll(Guid tenantId);
       int Insert(DeviceDto entity);
       bool Delete(DeviceDto entity);
       bool Find(DeviceDto entity);
       bool Update(DeviceDto entity);
   }
}
