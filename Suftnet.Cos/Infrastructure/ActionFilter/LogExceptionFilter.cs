namespace Suftnet.Cos.Service
{  
    using Suftnet.Cos.Core;
    using System;
    using System.Net.Http;
    using System.Web.Http.Filters;

    public class LogExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception == null)
            {
                return;
            }
            
            var ex = actionExecutedContext.Exception;
            var body = actionExecutedContext.Request.Content.ReadAsStringAsync().Result;

            ex.Data.Add("Request.Body", body);
            if (actionExecutedContext.Response != null)
            {
                ex.Data.Add("Response.ReasonPhrase", actionExecutedContext.Response.ReasonPhrase);
            }

            Logger(ex);

            var response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
            response.ReasonPhrase = "Fatal exception";
            response.Content = new StringContent("Internal Server Error");
            actionExecutedContext.Response = response;
        }

        #region private functions
        private void Logger(Exception exception)
            {
                var logger = GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>();
                logger.LogError(exception);
            }
        #endregion
    }
}