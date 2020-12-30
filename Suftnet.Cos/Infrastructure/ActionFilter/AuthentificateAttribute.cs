namespace Suftnet.Cos.Web.Infrastructure.ActionFilter
{
    using Suftnet.Cos.Core;
    using Suftnet.Cos.Services.Interface;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Web.Http;
    using System.Web.Http.Controllers;

    public class AuthentificateAttribute : AuthorizeAttribute
    {
        public string Realm { get; set; }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var request = actionContext.Request;
            var authorization = request.Headers.Authorization;

            if (authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Bad Request" });
                return;
            }

            if (authorization.Scheme != "Bearer")
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Bad Request" });
                return;
            }

            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, new { Message = "Bad Request" });
                return;
            }

            var token = authorization.Parameter;
            var principal = AuthenticateJwtToken(token);

            if (principal == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, new { Message = "You are not authorized to access this resource" });
                return;
            }
           
            actionContext.RequestContext.Principal = principal;
            base.OnAuthorization(actionContext);
        }        

        protected IPrincipal AuthenticateJwtToken(string token)
        {
            string username;

            var claimsIdentity = Validate(token, out username);
            if (claimsIdentity != null)
            {
                var claims = claimsIdentity.Claims;

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal user = new ClaimsPrincipal(identity);

                return user;
            }

            return null;
        }
        private ClaimsIdentity Validate(string token, out string username)
        {
            username = null;         
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

                var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userIdClaim?.Value))
                {
                    return null;
                }
            }

            return identity;
        }
      
    }
}