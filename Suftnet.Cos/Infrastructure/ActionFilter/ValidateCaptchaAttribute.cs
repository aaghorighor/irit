namespace Suftnet.Cos.Web.Infrastructure.ActionFilter
{
    using Suftnet.Cos.Core;
    using Suftnet.Cos.Web.Infrastructure.Captcha;
    using System.Net;
    using System.Web.Mvc;
  
    public sealed class ValidateCaptchaAttribute : ActionFilterAttribute
    {        
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context == null)
                return;

            ValidateCaptcha(context);
            base.OnActionExecuting(context);
        }

        #region private 
        private async void ValidateCaptcha(ActionExecutingContext context)
        {                         
            var captchaSecretKey = GeneralConfiguration.Configuration.Settings.CaptchaSecretKey;

            var token = context.HttpContext.Request.Form["token"];

            var service = new ReCaptchaService(captchaSecretKey);
            var responseJson =  await service.VerifyAsync(token, context.HttpContext.Request.UserHostAddress);
                     
            if(responseJson.StatusCode != HttpStatusCode.OK)
            {
                foreach (var error in responseJson.ErrorCodes)
                {
                    context.Controller.ViewData.ModelState.AddModelError("", error.ToString());
                }
            }      
        }       
        #endregion
    }
}