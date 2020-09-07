namespace Suftnet.Cos.Web.Services
{   
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.DataAccess.Identity;
    using System;
    using System.Threading.Tasks;

    public interface IApiUserManger
    {
        Task<ApplicationUser> CreateAsync(ApplicationUser model, Guid tenantId, string password, bool isSend, bool isBackoffice);
    }
}
