namespace Suftnet.Cos.CommonController.Controllers
{
    using Core;
       
    using Suftnet.Cos.Extension;
    using Suftnet.Cos.Common;
    using System.Linq;
    using System.Web.Mvc;
    using System.Security.Claims;
    using Web.Services.Interface;
    using Suftnet.Cos.DataAccess;

    public sealed class PermissionFilter : ActionFilterAttribute
    {
        private int viewId;
        private int permissionId;
        public PermissionFilter(int viewId, int permissionId)
        {
            this.viewId = viewId;
            this.permissionId = permissionId;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var identity = ((ClaimsIdentity)filterContext.HttpContext.User.Identity);
            var userId = identity.Claims.Where(x => x.Type == Identity.UserId).Select(x => x.Value).SingleOrDefault();
                       
            var permission = GeneralConfiguration.Configuration.DependencyResolver.GetService<IPermission>();
            var match = permission.Match(this.viewId, userId);
            if (match == null)
            {
                Chanllenge(filterContext);
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