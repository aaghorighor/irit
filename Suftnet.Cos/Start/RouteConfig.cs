namespace Suftnet.Cos.Web
{
    using Suftnet.Cos.Core;
    using System.Web.Mvc;
    using System.Web.Routing;
    using StructureMap;
    using Common;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes, IContainer container)
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.IgnoreRoute("{handler}.ashx");
            RouteTable.Routes.IgnoreRoute("robots.txt");
            RouteTable.Routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            RouteTable.Routes.IgnoreRoute("services/{*service}");
            RouteTable.Routes.IgnoreRoute("content/{*content}");
            RouteTable.Routes.IgnoreRoute("scripts/{*scripts}");
            RouteTable.Routes.IgnoreRoute("bundle/images/{*resources}");

            var iRoutesService = container.GetInstance<IRoutesService>();
            iRoutesService.Register();                               

          GeneralConfiguration.Configuration.Logger.Log("routes configured", EventLogSeverity.Information);
        }
    }
}