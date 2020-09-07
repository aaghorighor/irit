namespace Suftnet.Cos.UnitTest
{
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Http;
    using System.Web.Routing;
    using System.Net.Http;
 
    public static class ControllerExtensions
    {
        public static void SetupControllerContext(this Controller controller, HttpContextBase httpContext)
        {
            var routeData = new RouteData();
            var context = new ControllerContext(httpContext, routeData, controller);
            controller.ControllerContext = context;
            controller.Url = new UrlHelper(context.RequestContext, System.Web.Routing.RouteTable.Routes);
        }

        public static void SetupServiceControllerContext(this System.Web.Http.ApiController controller)
        {
            controller.Request = new HttpRequestMessage();
            controller.Request.Headers.Add("apikey", "test");
            controller.ControllerContext = new System.Web.Http.Controllers.HttpControllerContext();
        }

        public static string SuccessUrl(this System.Web.Mvc.Controller ctx)
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
    }
}
