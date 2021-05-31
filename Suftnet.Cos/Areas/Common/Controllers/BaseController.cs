namespace Suftnet.Cos.CommonController.Controllers
{
    using Core;
    using log4net;

    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Extension;
    using Suftnet.Cos.Web;

    using System;
    using System.Security.Claims;
    using System.Web.Mvc;
    using System.Linq;
  
    using Web.Infrastructure.ActionFilter;
    using System.Text;

    [ExceptionActionFilter]
    [RequireHttps]
    public class BaseController : OneChurchRoutes
    {
        public string UserName {

            get {

                var test = ((ClaimsIdentity)this.HttpContext.User.Identity);

                if (test != null)
                {
                    return test.Claims.Where(x => x.Type == Identity.FullName).Select(x => x.Value).SingleOrDefault();
                }

                return "UnKnown User";
            }      
        }

        public string CurrencyCode
        {
            get
            {

                var test = ((ClaimsIdentity)this.HttpContext.User.Identity);

                if (test != null)
                {
                    return test.Claims.Where(x => x.Type == Identity.CurrencyCode).Select(x => x.Value).SingleOrDefault();
                }

                return "£";
            }
        }

        public MvcHtmlString CurrencySysmbol
        {
            get
            {

                var test = ((ClaimsIdentity)this.HttpContext.User.Identity);

                if (test != null)
                {
                  var currencySymbol = test.Claims.Where(x => x.Type == Identity.CurrencyCode).Select(x => x.Value).SingleOrDefault();
                   return new MvcHtmlString(currencySymbol);
                }

                return new MvcHtmlString("£");
            }
        }
        public string UserId
        {
            get
            {
                var test = ((ClaimsIdentity)this.HttpContext.User.Identity);

                if (test != null)
                {
                    return test.Claims.Where(x => x.Type == Identity.UserId).Select(x => x.Value).SingleOrDefault();
                }

                return "UnKnown User";
            }
        }

        public string Email
        {
            get
            {
                var test = ((ClaimsIdentity)this.HttpContext.User.Identity);

                if (test != null)
                {
                    return test.Claims.Where(x => x.Type == Identity.Email).Select(x => x.Value).SingleOrDefault();
                }

                return "UnKnown User";
            }
        }

        public Guid TenantId
        {
            get
            {
                var test = ((ClaimsIdentity)this.HttpContext.User.Identity);

                if (test != null)
                {
                    var id=  test.Claims.Where(x => x.Type == Identity.TenantId).Select(x => x.Value).SingleOrDefault();
                    return new Guid(id);
                }

                return new Guid();
            }
        }

        public string TenantName
        {
            get
            {
                var test = ((ClaimsIdentity)this.HttpContext.User.Identity);

                if (test != null)
                {
                    var name = test.Claims.Where(x => x.Type == Identity.TenantName).Select(x => x.Value).SingleOrDefault();
                    return name;
                }

                return "";
            }
        }

        public string TenantEmail
        {
            get
            {
                var test = ((ClaimsIdentity)this.HttpContext.User.Identity);

                if (test != null)
                {
                    var name = test.Claims.Where(x => x.Type == Identity.TenantEmail).Select(x => x.Value).SingleOrDefault();
                    return name;
                }

                return "";
            }
        }

        public string TenantMobile
        {
            get
            {
                var test = ((ClaimsIdentity)this.HttpContext.User.Identity);

                if (test != null)
                {
                    var name = test.Claims.Where(x => x.Type == Identity.TenantMobile).Select(x => x.Value).SingleOrDefault();
                    return name;
                }

                return "";
            }
        }

        public string TenantAddress
        {
            get
            {
                var test = ((ClaimsIdentity)this.HttpContext.User.Identity);

                if (test != null)
                {
                    var name = test.Claims.Where(x => x.Type == Identity.CompleteAddress).Select(x => x.Value).SingleOrDefault();
                    return name;
                }

                return "";
            }
        }

        public string AppCode
        {
            get
            {
                var test = ((ClaimsIdentity)this.HttpContext.User.Identity);

                if (test != null)
                {
                    var name = test.Claims.Where(x => x.Type == Identity.AppCode).Select(x => x.Value).SingleOrDefault();
                    return name;
                }

                return "";
            }
        }
        protected override RedirectResult Redirect(string url)
        {
            if (url.IsNullOrEmpty())
            {
                throw new ArgumentException("Url cannot be null");
            }
            var result = base.Redirect(url);
            return result;
        }
        protected RedirectToRouteResult RedirectToSuftnetRoute(string routeName)
        {
            return RedirectToSuftnetRoute(routeName, null);
        }
        protected RedirectToRouteResult RedirectToSuftnetRoute(string routeName, object routeValues)
        {
            routeName = this.HttpContext.ResolveRouteName(routeName);
            var result = RedirectToRoute(routeName, routeValues);

            return result;
        }       
        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public JsonResult Logger(Exception ex)
        {                  
            GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>().LogError(ex);                        
            return Json(new { ok = false, msg = Constant.ErrorMessage }, JsonRequestBehavior.AllowGet);
        }

        public void LogError(Exception ex)
        {
            GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>().LogError(ex);
        }

        #region private function


        #endregion
    }
}
