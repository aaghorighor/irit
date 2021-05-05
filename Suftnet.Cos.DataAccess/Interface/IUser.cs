namespace Suftnet.Cos.DataAccess
{
    using DataAccess.Identity;
    using System;
    using System.Collections.Generic;

    public interface IUser
    {
        bool UpdateAccessCode(string userId, string otp);
        MobileTenantDto VerifyAccessCode(string otp, string emailAddress, string appCode);
        ApplicationUser GetUserByPhone(string phone, Guid tenantId);
        bool CheckEmailAddress(string userName);
        IList<UserAccountDto> GetById(Guid tenantId);
        IList<UserAccountDto> GetAll(Guid tenantId, int iskip, int itake, string isearch);
        IList<UserAccountDto> GetAll(Guid tenantId, int iskip, int itake);        
        ApplicationUser GetUserByUserName(string userName);
        ApplicationUser GetUserByUserId(string userId);         
        int Count(Guid TenantId);
        IList<UserAccountDto> GetAll(int iskip, int itake, string isearch);
        IList<UserAccountDto> GetAll(int iskip, int itake);
        int Count();
        ApplicationUser GetByUserId(string userId);
        bool CheckEmailAddress(string email, Guid tenantId);
        IList<UserAccountDto> Fetch(int iskip, int itake, string isearch, params int[] areaId);
        IList<UserAccountDto> Fetch(int iskip, int itake, params int[] areaId);
        bool Delete(string userId);
        ApplicationUser GetUserByUserName(string userName, string code);
        MobileTenantDto VerifyUser(Guid externalId, string userCode);
        bool Update(MobileUserDto entity);
    }
}
