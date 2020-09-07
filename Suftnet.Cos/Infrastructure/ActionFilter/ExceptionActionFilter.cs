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

                if (filterContext.Exception is DbEntityValidationException)
                {
                    var e = filterContext.Exception as DbEntityValidationException;

                    foreach (var eve in e.EntityValidationErrors)
                    {
                        logger.Log(string.Format(Constant.EntityValidationErrors,
                            eve.Entry.Entity.GetType().Name, eve.Entry.State), EventLogSeverity.Fatal);

                        foreach (var ve in eve.ValidationErrors)
                        {
                            logger.Log(string.Format(Constant.ValidationErrors,
                                ve.PropertyName, ve.ErrorMessage), EventLogSeverity.Fatal);
                        }
                    }
                }
                else
                {
                    if(filterContext.Exception.InnerException != null)
                    {
                        logger.LogError(filterContext.Exception.InnerException);
                    }
                    else
                    {
                        logger.LogError(filterContext.Exception);
                    }                    

                    if (!filterContext.Exception.Data.Contains("Controller"))
                    {
                        filterContext.Exception.Data.Add("Controller", filterContext.Controller.GetType().FullName);
                    }
                    if (!filterContext.Exception.Data.Contains("RawUrl"))
                    {
                        filterContext.Exception.Data.Add("RawUrl", filterContext.HttpContext.Request.RawUrl);
                    }                  
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