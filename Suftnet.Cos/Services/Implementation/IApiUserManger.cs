namespace Suftnet.Cos.Web.Services
{
    using Microsoft.AspNet.Identity;
    using Suftnet.Cos.DataAccess.Identity;
    using System;
  
    public interface IApiUserManger
    {
        ApplicationUser CreateAsync(UserManager<ApplicationUser> userManager,string viewPATH, string app_code, ApplicationUser model, Guid tenantId, string password, bool isSend, bool isBackoffice);
    }
}
