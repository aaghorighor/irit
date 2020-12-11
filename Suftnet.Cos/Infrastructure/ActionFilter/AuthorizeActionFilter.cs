namespace Suftnet.Cos.CommonController.Controllers
{
    using Common;  
    using Suftnet.Cos.Extension;
    using System;
    using System.Security.Claims;
    using System.Web.Mvc;
    using System.Linq;
    using Suftnet.Cos.Core;
    using Suftnet.Cos.DataAccess;

    public sealed class AuthorizeActionFilter : ActionFilterAttribute
    {
        private string[] _claims;
        public AuthorizeActionFilter(params string[] claims)
        {
            _claims = claims;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var identity = ((ClaimsIdentity)filterContext.HttpContext.User.Identity);

            if (identity == null)
            {
                Chanllenge(filterContext);
                return;
            }

            var claim = identity.Claims.Where(x => x.Type == Identity.AreaId).Select(x => x.Value).SingleOrDefault();

            if (string.IsNullOrEmpty(claim))
            {
                Chanllenge(filterContext);
                return;
            }

            string line = _claims.FirstOrDefault();
            var test = line.Contains(claim);
           
            if (!test)
            {
                Chanllenge(filterContext);
                return;
            }

            var userId = identity.Claims.Where(x => x.Type == Identity.UserId).Select(x => x.Value).SingleOrDefault();

            if (string.IsNullOrEmpty(userId))
            {
                Chanllenge(filterContext);
                return;
            }

            var isExpired = identity.Claims.Where(x => x.Type == Identity.IsExpired).Select(x => x.Value).SingleOrDefault();

            if (isExpired.ToBoolean())
            {
                filterContext.HttpContext.Response.Redirect(filterContext.SubscriptionUrl());
                return;
            }

            var expirationDate = identity.Claims.Where(x => x.Type == Identity.ExpirationDate).Select(x => x.Value).SingleOrDefault();

            if (string.IsNullOrEmpty(expirationDate))
            {
                Chanllenge(filterContext);
                return;
            }

            var date = expirationDate.ToDate();          
            if (date == null)
            {
                Chanllenge(filterContext);
                return;
            }

            if(date.Date < DateTime.UtcNow.Date)
            {
                var tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
                var tenantId = identity.Claims.Where(x => x.Type == Identity.TenantId).Select(x => x.Value).SingleOrDefault();
                tenant.UpdateStatus(new Guid(tenantId), true);

                filterContext.HttpContext.Response.Redirect(filterContext.SubscriptionUrl());
                return;
            }           

            base.OnActionExecuting(filterContext);
        }

        #region private
        private void Chanllenge(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        ok = false,                    
                        msg = "Access Denied."
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    ContentType = "application/json; charset=utf-8"
                };
            }
            else
            {
                if (filterContext.RequestContext.HttpContext.Request.UrlReferrer != null)
                {
                    filterContext.Result = new RedirectResult(filterContext.RequestContext.HttpContext.Request.UrlReferrer.AbsoluteUri);
                }
                else
                {
                    filterContext.Result = new RedirectResult(filterContext.LoginUrl());
                }
            }
         
        }
        #endregion
    }
}