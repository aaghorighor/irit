namespace Suftnet.Cos.Web.Infrastructure.ActionFilter
{
    using Common;
    using Core;
    using System.Web.Mvc;
    using Suftnet.Cos.Extension;
    using System.Data.Entity.Validation;

    public class ExceptionActionFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if(filterContext.Exception != null)
            {
                var logger = GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>();

                if (!filterContext.Exception.Data.Contains("Controller"))
                {
                    filterContext.Exception.Data.Add("Controller", filterContext.Controller.GetType().FullName);
                }
                if (!filterContext.Exception.Data.Contains("RawUrl"))
                {
                    filterContext.Exception.Data.Add("RawUrl", filterContext.HttpContext.Request.RawUrl);
                }

                if (filterContext.Exception.InnerException != null)
                {
                    logger.LogError(filterContext.Exception.InnerException);
                }
                else
                {
                    logger.LogError(filterContext.Exception);
                }               

                Chanllenge(filterContext);
            }

            base.OnException(filterContext);         
        }

        #region private
        private void Chanllenge(ExceptionContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        ok = false,                   
                        msg = Constant.ErrorMessage
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    ContentType = "application/json; charset=utf-8"
                };
            }
            else
            {
                filterContext.HttpContext.Response.Redirect(filterContext.ErrorUrl());
            }
            return;
        }
        #endregion
    }
}