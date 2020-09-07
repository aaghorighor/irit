namespace Suftnet.Cos.Web
{
    using Suftnet.Cos.Common;
    using Suftnet.Cos.Core;
    using Suftnet.Cos.Extension;

    using System;
    using System.Collections.Generic;
    using System.Web;

    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;   

    /// <summary>
    /// Méthodes d'extensions pour la determination des routes
    /// </summary>
    public static class RouteExtensions
    {        
        public static string ResolveRouteName(this HtmlHelper htmlHelper, string routeName)
        {
            return ResolveRouteName(htmlHelper.ViewContext.HttpContext, routeName);
        }
        
        public static string ResolveRouteName(this AjaxHelper helper, string routeName)
        {
            return ResolveRouteName(helper.ViewContext.HttpContext, routeName);
        }
             
        public static string ResolveRouteName(this UrlHelper helper, string routeName)
        {
            return ResolveRouteName(helper.RequestContext.HttpContext, routeName);
        }
      
        public static string ResolveRouteName(this HttpContextBase context, string routeName)
        {           
            return routeName;
        }

        public static MvcForm BeginOneChurchForm(this HtmlHelper htmlHelper, string routeName, FormMethod method)
        {
            routeName = htmlHelper.ResolveRouteName(routeName);
            return htmlHelper.BeginRouteForm(routeName, method);
        }

        public static MvcForm BeginOneChurchForm(this HtmlHelper htmlHelper, string routeName, FormMethod method, object htmlAttributes)
        {
            routeName = htmlHelper.ResolveRouteName(routeName);
            return htmlHelper.BeginRouteForm(routeName, method, htmlAttributes);
        }

        public static MvcForm BeginOneChurchForm(this HtmlHelper htmlHelper, string routeName, object routeValues, FormMethod method)
        {
            routeName = htmlHelper.ResolveRouteName(routeName);
            return htmlHelper.BeginRouteForm(routeName, routeValues, method);
        }

        public static MvcForm BeginOneChurchForm(this AjaxHelper helper, string routeName, AjaxOptions options)
        {
            return helper.BeginOneChurchForm(routeName, null, options, null);
        }

        public static MvcForm BeginOneChurchForm(this AjaxHelper helper, string routeName, object routeValues, AjaxOptions options, object htmlAttributes)
        {
            routeName = helper.ResolveRouteName(routeName);
            var result = helper.BeginRouteForm(routeName, routeValues, options, htmlAttributes);
            return result;
        }

        public static string RouteOneChurchLink(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues)
        {
            routeName = htmlHelper.ResolveRouteName(routeName);
            return htmlHelper.RouteLink(linkText, routeName, routeValues).ToHtmlString();
        }

        public static string RouteOneChurcheLink(this HtmlHelper htmlHelper, string linkText, string routeName, RouteValueDictionary routeValues)
        {
            routeName = htmlHelper.ResolveRouteName(routeName);
            return htmlHelper.RouteLink(linkText, routeName, routeValues, routeValues).ToHtmlString();
        }

        public static string RouteOneChurchLink(this HtmlHelper htmlHelper, string linkText, string routeName, object routeValues, object htmlAttributes)
        {
            routeName = htmlHelper.ResolveRouteName(routeName);
            return htmlHelper.RouteLink(linkText, routeName, routeValues, htmlAttributes).ToHtmlString();
        }

        public static string RouteOneChurchLink(this HtmlHelper htmlHelper, string linkText, string routeName, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes)
        {
            routeName = htmlHelper.ResolveRouteName(routeName);
            return htmlHelper.RouteLink(linkText, routeName, routeValues, htmlAttributes).ToHtmlString();
        }

        public static string RouteOneChurchLink(this HtmlHelper htmlHelper, string linkText, string routeName, string protocol, string hostName, string fragment, object routeValues, object htmlAttributes)
        {
            routeName = htmlHelper.ResolveRouteName(routeName);
            return htmlHelper.RouteLink(linkText, routeName, protocol, hostName, fragment, routeValues, htmlAttributes).ToHtmlString();
        }

        public static string RouteOneChurchLink(this HtmlHelper htmlHelper
            , string linkText
            , string routeName
            , string protocol
            , string hostName
            , string fragment
            , RouteValueDictionary routeValues
            , IDictionary<string, object> htmlAttributes)
        {
            routeName = htmlHelper.ResolveRouteName(routeName);
            string result = "#";
            try
            {
                result = htmlHelper.RouteLink(linkText, routeName, protocol, hostName, fragment, routeValues, htmlAttributes).ToHtmlString();
            }
            catch (Exception ex)
            {
                var logger = GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>();
                logger.LogError(ex);
            }
            return result;
        }

        public static string RouteOneChurchUrl(this UrlHelper helper, string routeName)
        {
            return helper.RouteOneChurchUrl(routeName, null);
        }

        public static string RouteOneChurchUrl(this UrlHelper helper, string routeName, object routeValues)
        {
            var resolvedRoute = helper.ResolveRouteName(routeName);
            string result = "#";
            var existingRoute = RouteTable.Routes.GetByName(resolvedRoute);
            if (existingRoute == null)
            {
                var logger = GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>();
                if (resolvedRoute.Equals(routeName, StringComparison.InvariantCultureIgnoreCase))
                {
                    logger.Log(string.Format("RouteOneChurchUrl {0} does not exists {1}/{2}", routeName, helper.RequestContext.HttpContext.Request.UserHostAddress, helper.RequestContext.HttpContext.Request.UserAgent), EventLogSeverity.Warning);
                    return result;
                }
                resolvedRoute = routeName;
            }
            else
            {
                try
                {
                    result = helper.RouteUrl(resolvedRoute, routeValues);
                }
                catch (Exception ex)
                {
                    var logger = GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>();
                    ex.Data.Add("RouteName", routeName);
                    logger.LogError(ex);
                }
            }
            return result;
        }

    }
}