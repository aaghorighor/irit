namespace Suftnet.Cos.Extension
{
    using Suftnet.Cos.Core;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Web;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Filters;
    using System.Web.Routing;
    using System.Web.Security;

    public static class HttpExtensions
    {
        /// <summary>
        /// Return the first the or default cached item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache">The cache.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public static T FirstOrDefault<T>(this System.Web.Caching.Cache cache, Func<T, bool> predicate)
        {
            var list = cache.Cast<System.Collections.DictionaryEntry>()
                .Where(i => i.Value.GetType() == typeof(T))
                .Select(i => i.Value).Cast<T>();

            return list.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Gets the list of.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache">The cache.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public static IEnumerable<T> GetListOf<T>(this System.Web.Caching.Cache cache, Func<T, bool> predicate)
        {
            var list = cache.Cast<System.Collections.DictionaryEntry>()
                        .Where(i => i.Value.GetType() == typeof(T))
                        .Select(i => i.Value).Cast<T>();

            return list.Where(predicate);
        }

        /// <summary>
        /// Gets the list of.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache">The cache.</param>
        /// <returns></returns>
        public static IQueryable<T> GetListOf<T>(this System.Web.Caching.Cache cache)
        {
            var list = cache.Cast<System.Collections.DictionaryEntry>()
                        .Where(i => i.Value.GetType() == typeof(T))
                        .Select(i => i.Value).Cast<T>();

            return list.AsQueryable();
        }

     
        public static string GetControllerActionName(this HttpContextBase context)
        {
            RouteData currentRoute = null;
            try
            {
                currentRoute = RouteTable.Routes.GetRouteData(context); // as System.Web.Routing.RouteData;
            }
            catch
            {
                /* Tests */
            }
            if (currentRoute == null)
            {
                return null;
            }
            string controller = string.Format("{0}", currentRoute.Values["controller"] ?? "home");
            string action = string.Format("{0}", currentRoute.Values["action"] ?? "index");
            var controllerActionName = string.Format("{0}/{1}", controller, action);
            return controllerActionName.ToLower();
        }       
      
        public static System.Web.Mvc.UrlHelper GetUrlHelper(this System.Web.Mvc.Controller ctx)
        {
            if (ctx == null)
            {
                return null;
            }

            var routeData = new RouteData();
            var requestContext = new System.Web.Routing.RequestContext(ctx.HttpContext, routeData);
            var urlHelper = new UrlHelper(requestContext);          

            return urlHelper;
        }           

        public static string LoginUrl(this ActionExecutingContext context)
        {
            if (context == null)
            {
                return null;
            }
         
            var urlHelper = new UrlHelper(context.RequestContext);
            var url = urlHelper.Action("Index", "Account", new { area = "" });
            return url;
        }

        public static string ErrorUrl(this ExceptionContext context)
        {
            if (context == null)
            {
                return null;
            }

            var urlHelper = new UrlHelper(context.RequestContext);
            var url = urlHelper.Action("Index", "Error", new { area = "" });
            return url;
        }

        public static string DefaultLoginUrl(this ActionExecutingContext context)
        {
            if (context == null)
            {
                return null;
            }

            return "/";
        }

        public static string DefaultLoginUrl(this AuthenticationChallengeContext context)
        {
            if (context == null)
            {
                return null;
            }

            return "/";
        }


        public static string SubscriptionUrl(this ActionExecutingContext context)
        {
            if (context == null)
            {
                return null;
            }

            var urlHelper = new UrlHelper(context.RequestContext);
            var url = urlHelper.Action("index", "dashboard", new { area = "Subscription" });
            return url;
        }

        public static string FrontOfficeUrl(this System.Web.Mvc.Controller ctx)
        {
            if (ctx == null)
            {
                return null;
            }

            var routeData = new RouteData();
            var requestContext = new System.Web.Routing.RequestContext(ctx.HttpContext, routeData);
            var urlHelper = new UrlHelper(requestContext);
            var url = urlHelper.Action("Index", "Dashboard", new { area = "FrontOffice" });
            return url;
        }
        public static string SiteAdminOfficeUrl(this System.Web.Mvc.Controller ctx)
        {
            if (ctx == null)
            {
                return null;
            }

            var routeData = new RouteData();
            var requestContext = new System.Web.Routing.RequestContext(ctx.HttpContext, routeData);
            var urlHelper = new UrlHelper(requestContext);
            var url = urlHelper.Action("Index", "Dashboard", new { area = "SiteAdmin" });
            return url;
        }
        public static string BackOfficerUrl(this System.Web.Mvc.Controller ctx)
        {
            if (ctx == null)
            {
                return null;
            }

            var routeData = new RouteData();
            var requestContext = new System.Web.Routing.RequestContext(ctx.HttpContext, routeData);
            var urlHelper = new UrlHelper(requestContext);
            var url = urlHelper.Action("Index", "Dashboard", new { area = "BackOffice" });
            return url;
        }

        public static string LoginLink(this System.Web.Http.ApiController ctx)
        {
            if (ctx == null)
            {
                return null;
            }

            var url = ctx.Url.Link("Home", new { Controller = "Account", Action = "LogOn" });

            return url;
        }

        public static string AdminUrl(this System.Web.Mvc.Controller ctx)
        {
            if (ctx == null)
            {
                return null;
            }

            var routeData = new RouteData();
            var requestContext = new System.Web.Routing.RequestContext(ctx.HttpContext, routeData);
            var urlHelper = new UrlHelper(requestContext);
            var url = urlHelper.Action("Index", "Dashboard", new { area = "Admin" });
            return url;
        }

        public static string DefaultLoginUrl(this System.Web.Mvc.Controller context)
        {
            if (context == null)
            {
                return null;
            }

            var routeData = new RouteData();
            var requestContext = new System.Web.Routing.RequestContext(context.HttpContext, routeData);
            var urlHelper = new UrlHelper(requestContext);
            var url = urlHelper.Action("Index", "Account", new { area = "" });
            return url;
        }


        public static string DefaultUrl(this System.Web.Mvc.Controller ctx)
        {
            if (ctx == null)
            {
                return null;
            }

            var routeData = new RouteData();
            var requestContext = new System.Web.Routing.RequestContext(ctx.HttpContext, routeData);
            var urlHelper = new UrlHelper(requestContext);
            var url = urlHelper.Action("Index", "Home", new { area = "" });
            return url;
        }

        public static HttpRequestBase GetRequestBase(this HttpRequestMessage request)
        {
            if (request == null
                || request.Properties == null)
            {
                return null;
            }

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request;
            }
            return null;
        }

        public static string GetClientIpAddress(this HttpRequestMessage request)
        {
            var requestBase = request.GetRequestBase();
            if (requestBase != null)
            {
                return requestBase.UserHostAddress;
            }           
            else
            {
                return null;
            }
        }         
     
        public static Exception GetLastException(this HttpServerUtility server, System.Web.HttpContext ctx)
        {
            var ex = server.GetLastError();

            if (ex.Message.IndexOf("A potentially dangerous Request") != -1)
            {
                return null;
            }          
            ex.Data.Add("Message", ex.Message);
            ex.Data.Add("machineName", Environment.MachineName);
            ex.Data.Add("host", ctx.Request.Url.Host);          
            ex.Data.Add("userHostAddress", ctx.Request.UserHostAddress);
            ex.Data.Add("userHostName", ctx.Request.UserHostName);
            ex.Data.Add("url", ctx.Request.RawUrl);
            ex.Data.Add("referer", ctx.Request.UrlReferrer);
            ex.Data.Add("applicationPath", ctx.Request.ApplicationPath);
            ex.Data.Add("user-agent", ctx.Request.Headers["User-Agent"]);
            ex.Data.Add("cookie", ctx.Request.Headers["Cookie"]);
            ex.Data.Add("httpmethod", ctx.Request.HttpMethod.ToString());
            if (ctx.Request.Form.Count > 0)
            {
                ex.Data.Add("begin-form", "-----------------------");
                int i = 1;
                foreach (var item in ctx.Request.Form.AllKeys)
                {
                    string key = string.Format("{0}:{1}", item, i);
                    ex.Data.Add(key, ctx.Request.Form[item]);
                }
                ex.Data.Add("end-form", "-----------------------");
            }         

            return ex;
        }
    }
}