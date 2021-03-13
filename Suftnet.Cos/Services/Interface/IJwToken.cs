using System.Security.Claims;

namespace Suftnet.Cos.Services.Interface
{
    public interface IJwToken
    {
        string Create(string username, string userId, string tenantId);
        ClaimsPrincipal Principal(string token);
    }
}
