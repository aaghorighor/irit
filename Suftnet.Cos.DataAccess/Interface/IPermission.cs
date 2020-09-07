namespace Suftnet.Cos.DataAccess
{
    using System;
    using System.Collections.Generic;
    public interface IPermission
   {
        bool Clear(string userId);
        bool Delete(Guid id);
        List<PermissionDto> GetByUserId(string userId);
        Guid Insert(PermissionDto entity);
        bool Update(PermissionDto entity);
        PermissionDto Get(Guid id);
        PermissionDto Match(int viewId, string userId);
       
   }
}
