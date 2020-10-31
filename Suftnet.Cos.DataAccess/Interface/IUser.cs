namespace Suftnet.Cos.DataAccess
{
    using DataAccess.Identity;
    using System;
    using System.Collections.Generic;

    public interface IUser
    {
        ApplicationUser VerifyAccessCode(string otp, string phone, Guid tenantId);
        bool UpdateAccessCode(string phoneNumber, Guid tenantId, string otp);
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
        IList<UserAccountDto> Fetch(int areaId, int iskip, int itake, string isearch);
        IList<UserAccountDto> Fetch(int areaId, int iskip, int itake);
        bool Delete(string userId);
    }
}
