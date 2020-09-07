namespace Suftnet.Cos.CommonController.Controllers
{
    using Suftnet.Cos.Extension;
    using System.Security.Claims;
    using System.Web.Mvc;
    using System.Linq;
    using Common;

    public sealed class AdminAuthorizeActionFilter : ActionFilterAttribute
    {
        private string[] _claims;
        public AdminAuthorizeActionFilter(params string[] claims)
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
                        error = 1,
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