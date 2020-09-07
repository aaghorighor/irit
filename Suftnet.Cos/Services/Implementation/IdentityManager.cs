namespace Suftnet.Cos.Web.Services.Interface
{
    using System.Collections.Generic;
    using System.Security.Claims;

    using Common;  
    using Microsoft.AspNet.Identity;
    using DataAccess.Identity;
    using DataAccess;

    public class ClaimManager : IClaimManager
    {
        private readonly IUserAccount _memberAccount;
        private readonly ITenant _tenant;

        public ClaimManager(IUserAccount memberAccount, ITenant tenant)
        {
            _memberAccount = memberAccount;
            _tenant = tenant;
        }

        public ClaimsIdentity Principal(ApplicationUser user)
        {
            var identity = new ClaimsIdentity(this.add(user), DefaultAuthenticationTypes.ApplicationCookie);
            var principal = new ClaimsPrincipal(identity);

            return identity;
        }

        public IEnumerable<Claim> add(ApplicationUser user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim("UserId", user.Id));
            claims.Add(new Claim("FirstName", user.FirstName));
            claims.Add(new Claim("LastName", user.LastName));
            claims.Add(new Claim("FullName", user.FirstName + " " + user.LastName));          
            claims.Add(new Claim("UserName", user.UserName));           
            claims.Add(new Claim("AreaId", user.AreaId.ToString()));
            claims.Add(new Claim("Email", user.Email));
            
            AddMoreClaim(claims, user);

            return claims;
        }

        private void AddMoreClaim(List<Claim> claims, ApplicationUser user)
        {
            if(user.AreaId == (int)eArea.Admin ||
               user.AreaId == (int)eArea.SiteAdmin)
            {
                return;
            }

            var account = _memberAccount.GetByUserId(user.Id);        

            if (account != null)
            {
                var tenant = _tenant.Get(account.TenantId);

                claims.Add(new Claim("TenantId", account.TenantId.ToString()));
                claims.Add(new Claim("IsExpired", account.IsExpired.ToString()));
                claims.Add(new Claim("ExpirationDate", account.ExpirationDate.ToString()));
                claims.Add(new Claim("TenantName", account.TenantName));
                claims.Add(new Claim("CurrencyCode", tenant.CurrencyCode ?? Constant.DefaultHexCurrencySymbol));
            }           
        }
    }

}