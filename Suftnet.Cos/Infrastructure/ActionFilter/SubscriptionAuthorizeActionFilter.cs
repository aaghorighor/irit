namespace Suftnet.Cos.CommonController.Controllers
{
    using Common;
    using Core;
    using Suftnet.Cos.Extension;
    using System;
    using System.Security.Claims;
    using System.Web.Mvc;
    using System.Linq;
    using Web.Services.Interface;
    using System.IO;

    public sealed class SubscriptionAuthorizeActionFilter : ActionFilterAttribute
    {
        private string[] _claims;
        public SubscriptionAuthorizeActionFilter(params string[] claims)
        {
            _claims = claims;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var identity = ((ClaimsIdentity)filterContext.HttpContext.User.Identity);

            if (identity == null)
            {
                Chanllenge(filterContext);
            }

            var claim = identity.Claims.Where(x => x.Type == Identity.AreaId).Select(x => x.Value).SingleOrDefault();

            if (string.IsNullOrEmpty(claim))
            {
                Chanllenge(filterContext);
            }

            string line = _claims.FirstOrDefault();
            var test = line.Contains(claim);
           
            if (!test)
            {
                Chanllenge(filterContext);
            }

            var userId = identity.Claims.Where(x => x.Type == Identity.UserId).Select(x => x.Value).SingleOrDefault();

            if (string.IsNullOrEmpty(userId))
            {
                Chanllenge(filterContext);
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
                    filterContext.HttpContext.Response.Redirect(filterContext.RequestContext.HttpContext.Request.UrlReferrer.AbsoluteUri);
                }
                else
                {
                    filterContext.HttpContext.Response.Redirect(filterContext.LoginUrl());
                }
            }
            return;
        }
        #endregion
    }
}