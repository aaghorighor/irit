namespace Suftnet.Cos.Web.ActionFilter
{
    using Core;
    using Cos.Services.Interface;
    using System;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Filters;

    public class JwtAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public string Realm { get; set; }
        public bool AllowMultiple => false;          
       
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            if (authorization == null || authorization.Scheme != "Bearer")
                return;

            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing Jwt Token", request);
                return;
            }

            var token = authorization.Parameter;
            var principal = await AuthenticateJwtToken(token);

            if (principal == null)
                context.ErrorResult = new AuthenticationFailureResult("Invalid token", request);

            else
                context.Principal = principal;
        }       

        protected Task<IPrincipal> AuthenticateJwtToken(string token)
        {
            string username;

            var claimsIdentity = Validate(token, out username);
            if (claimsIdentity != null)
            {                
                var claims = claimsIdentity.Claims;

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal user = new ClaimsPrincipal(identity);

                return Task.FromResult(user);
            }

            return Task.FromResult<IPrincipal>(null);
        }

        private ClaimsIdentity Validate(string token, out string username)
        {
            username = null;
            var userId = string.Empty;
            var _iJwToken = GeneralConfiguration.Configuration.DependencyResolver.GetService<IJwToken>();

            var principle = _iJwToken.Principal(token);
            var identity = principle?.Identity as ClaimsIdentity;

            if (identity != null && identity.IsAuthenticated)
            {
                var claim = identity.FindFirst(ClaimTypes.Name);

                if (string.IsNullOrEmpty(claim?.Value))
                {
                    return null;
                }

                var claimSid = identity.FindFirst(ClaimTypes.GroupSid);

                if (string.IsNullOrEmpty(claimSid?.Value))
                {
                    return null;
                }

                var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    return null;
                }
            }

            return identity;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            Challenge(context);
            return Task.FromResult(0);
        }

        private void Challenge(HttpAuthenticationChallengeContext context)
        {
            string parameter = null;

            if (!string.IsNullOrEmpty(Realm))
                parameter = "realm=\"" + Realm + "\"";

            context.ChallengeWith("Bearer", parameter);
        }
    }
}