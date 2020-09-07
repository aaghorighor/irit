namespace Suftnet.Cos.Web.Services.Implementation
{
    using Core;  
    using DataAccess.Identity;
    using Interface;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin;
    using Microsoft.Owin.Security;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {                 
            return UserIdentityAsync(user, (ApplicationUserManager)UserManager); 
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }

        public async Task<ClaimsIdentity> UserIdentityAsync(ApplicationUser user, UserManager<ApplicationUser> manager)
        {
            var test = GeneralConfiguration.Configuration.DependencyResolver.GetService<IClaimManager>();
            var userIdentity = await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie).ConfigureAwait(false);
            var claims = test.add(user);
            userIdentity.AddClaims(claims);
            return userIdentity;        }


    }
}