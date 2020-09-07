namespace Suftnet.Cos.Web.Services.Interface
{  
    using DataAccess.Identity;
    using System.Collections.Generic;
    using System.Security.Claims;

    public interface IClaimManager
    {
        IEnumerable<Claim> add(ApplicationUser user);
        ClaimsIdentity Principal(ApplicationUser user);
    }    

}