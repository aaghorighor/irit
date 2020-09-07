namespace Suftnet.Cos.DataAccess
{
    using Suftnet.Cos.DataAccess.Identity;
    using System;
    using System.Collections.Generic;
    public interface IUserAccount
    {           
        int Insert(Action.UserAccount entity);
        UserAccountDto GetByUserName(string userName);
        List<UserAccountDto> GetById(Guid tenantId);
        UserAccountDto GetByUserId(string userId);
        bool Delete(Guid tenantId);      
    }
}
