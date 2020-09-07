namespace Suftnet.Cos.Web.Services.Implementation
{
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.AspNet.Identity.EntityFramework;
    using DataAccess.Identity;
    using DataAccess.Action;

    public class AppRoleManager : RoleManager<ApplicationRole>
    {
        public AppRoleManager(IRoleStore<ApplicationRole> store)
        : base(store)
        {
        }

        public static RoleManager<IdentityRole> Create(
        IdentityFactoryOptions<AppRoleManager> options, IOwinContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new DataContext()));
            return roleManager;
        }
    }
}