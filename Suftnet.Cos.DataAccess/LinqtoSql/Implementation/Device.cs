namespace Suftnet.Cos.DataAccess
{
    using System.Collections.Generic;
    using System.Linq;
    using Suftnet.DataFactory.LinqToSql;
    using System.Data.Linq;
    using System;

    public class Device : IDevice
    {      
        public bool Delete(DeviceDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToDelete = context.Devices.FirstOrDefault(o => o.Serial == entity.Serial);
                if (objToDelete != null)
                {
                    context.Devices.Remove(objToDelete);
                    context.SaveChanges();
                    response = true;
                }
            }
            return response;
        }

        public bool Find(DeviceDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToFind = context.Devices.FirstOrDefault(o => o.Serial == entity.Serial);
                if (objToFind != null)
                {                  
                    response = true;
                }
            }
            return response;
        }

        public int Insert(DeviceDto entity)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var obj = new Action.Device() { TenantId = entity.TenantId, AppVersion = entity.AppVersion, DeviceId = entity.DeviceId, DeviceName = entity.DeviceName, OsVersion = entity.OsVersion, Serial = entity.Serial, CreatedBy = entity.CreatedBy, CreatedDT = entity.CreatedDT };
                context.Devices.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }

        public bool Update(DeviceDto entity)
        {
            bool response = false;
            using (var context = DataContextFactory.CreateContext())
            {
                var objToUpdate = context.Devices.SingleOrDefault(o => o.Serial == entity.Serial);
                if (objToUpdate != null)
                {
                    objToUpdate.DeviceId = entity.DeviceId;
                    objToUpdate.OsVersion = entity.OsVersion;
                    objToUpdate.DeviceName = entity.DeviceName;                   

                    try
                    {
                        context.SaveChanges();
                        response = true;
                    }
                    catch (ChangeConflictException)
                    {           
                        response = true;
                    }
                }
            }
            return response;
        }

        public List<DeviceDto> GetAll(Guid tenantId)
        {
            using (var context = DataContextFactory.CreateContext())
            {
                var objResult = (from o in context.Devices
                                 where o.TenantId == tenantId
                                 orderby o.Id descending
                                 select new DeviceDto { DeviceId = o.DeviceId, OsVersion = o.OsVersion, Serial = o.Serial, AppVersion = o.AppVersion, Id = o.Id }).ToList();
                return objResult;
            }
        }
    }
}
