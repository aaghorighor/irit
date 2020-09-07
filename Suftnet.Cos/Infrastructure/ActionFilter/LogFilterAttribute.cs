namespace Suftnet.Cos.Service
{
    using Suftnet.Cos.Common;
    using Suftnet.Cos.Core;
    using System.Web.Mvc;

    public class LogFilterAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var logger = GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>();
            logger.Log(string.Format(
                "{0} ActionMethod on {1} Controller executing...",
                actionContext.ActionDescriptor.ActionName,
                actionContext.ActionDescriptor.ControllerDescriptor.ControllerName), EventLogSeverity.Information);
        }
    }
}