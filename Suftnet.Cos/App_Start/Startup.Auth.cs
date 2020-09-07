namespace Suftnet.Cos.Web
{
    using Suftnet.Cos.DataAccess.Action;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security.Cookies;
    using Owin;
    using Services.Implementation;
    using System;
    using DataAccess.Identity;
    using Core;

    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            try
            {
                app.CreatePerOwinContext(DataContext.Create);
                app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
                app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
                app.CreatePerOwinContext<RoleManager<ApplicationRole>>((options, context) =>
                     new RoleManager<ApplicationRole>(
                         new RoleStore<ApplicationRole>(new DataContext())));

                app.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    SlidingExpiration = true,
                    CookieHttpOnly = false,
                    LoginPath = new PathString("/test/index"),
                    Provider = new CookieAuthenticationProvider
                    {
                        //OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        //        validateInterval: TimeSpan.FromMinutes(30),
                        //            regenerateIdentity: (manager, user) =>
                        //            user.GenerateUserIdentityAsync(manager)),

                        //OnResponseSignIn = context =>
                        //{
                        //    context.Properties.IsPersistent = true;
                        //}
                    }
                });
            }
            catch(Exception ex)
            {
                GeneralConfiguration.Configuration.Logger.LogError(ex);
            }           
        }
    }
}