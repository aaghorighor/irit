using System.Security.Claims;

namespace Suftnet.Cos.Services.Interface
{
    public interface IJwToken
    {
        string Create(string username, string userId);
        ClaimsPrincipal Principal(string token);
    }
}
