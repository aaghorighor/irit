namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    public interface IMobilePermission
    {
        bool Find(MobilePermissionDto entity);
        bool Clear(string userId);
        bool Delete(Guid id);
        List<MobilePermissionDto> GetByUserId(string userId);
        Guid Insert(MobilePermissionDto entity);
        bool Update(MobilePermissionDto entity);
        MobilePermissionDto Get(Guid id);
        string[] GetPermissionByUserId(string userId);
   }
}
