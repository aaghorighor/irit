namespace Suftnet.Cos.Web
{ 
    using Suftnet.Cos.Core;
    using Suftnet.Cos.DataAccess;
    
    using System;

    public class SiteConfig
    {
        public static void Configure(System.Web.HttpContextBase ctx)
        {
            var global = GeneralConfiguration.Configuration.DependencyResolver.GetService<IGlobal>();          
            var settings = new Core.Settings();

            settings.General = global.Get();

            if (settings == null) 
            {
                throw new Exception("Settings does not exists");
            }

            var tempPath = ConfigurationSettings.AppSettings["tempPath"] ?? System.IO.Path.GetTempPath();
            if (tempPath.StartsWith(@"~\"))
            {
                tempPath = ctx.Server.MapPath(tempPath);
            }
            settings.TempPath = tempPath;

            var imagePath = ConfigurationSettings.AppSettings["imagePath"] ?? tempPath;
            if (imagePath.StartsWith(@"~\"))
            {
                imagePath = ctx.Server.MapPath(imagePath);
            }
            settings.ImagePath = imagePath;

            var documentPath = ConfigurationSettings.AppSettings["documentPath"] ?? tempPath;
            if (documentPath.StartsWith(@"~\"))
            {
                documentPath = ctx.Server.MapPath(documentPath);
            }
            settings.DocumentPath = documentPath;

            settings.CurrentUrl = string.Format("http://{0}", ctx.Request.Url.Host);
            settings.PhysicalPath = ctx.Server.MapPath("/");

            settings.TwilioAccountSid = ConfigurationSettings.AppSettings["TwilioAccountSid"];
            settings.TwilioAuthToken = ConfigurationSettings.AppSettings["TwilioAuthToken"];
            settings.TwilioPhoneNumber = ConfigurationSettings.AppSettings["TwilioPhoneNumber"];
            settings.TwilioImageUrl = ConfigurationSettings.AppSettings["TwilioImageUrl"];

            settings.SendGridApi = ConfigurationSettings.AppSettings["SendGridAPIKey"];

            settings.StripeSecretKey = ConfigurationSettings.AppSettings["StripeSecretKey"];
            settings.StripePublishableKey = ConfigurationSettings.AppSettings["StripePublishableKey"];

            settings.RequestMessage = ConfigurationSettings.AppSettings["RequestMessage"];
            settings.ResponseMessage = ConfigurationSettings.AppSettings["ResponseMessage"];

            settings.PushNotificationServerkey = ConfigurationSettings.AppSettings["PushNotificationServerkey"];
            settings.PushNotificationSenderId = ConfigurationSettings.AppSettings["PushNotificationSenderId"];
            settings.PushNotificationUrl = ConfigurationSettings.AppSettings["PushNotificationUrl"];

            settings.CaptchaSiteKey = ConfigurationSettings.AppSettings["CaptchaSiteKey"];
            settings.CaptchaSecretKey = ConfigurationSettings.AppSettings["CaptchaSecretKey"];

            settings.MobileLink = ConfigurationSettings.AppSettings["mobilelink"];
            settings.OnlineLink = ConfigurationSettings.AppSettings["onlinelink"];

            try
            {              
                Stimulsoft.Base.StiLicense.LoadFromFile(ctx.Server.MapPath("~/App_Data/license/stimulsoft/license.key"));
            }
            catch (Exception ex)
            { GeneralConfiguration.Configuration.Logger.LogError(ex); }

            GeneralConfiguration.Configuration.HosterName = ConfigurationSettings.AppSettings["HostName"] ?? "Irit";
            GeneralConfiguration.Configuration.Settings = settings;
            GeneralConfiguration.Configuration.Logger.Log("Website configuration enabled", Cos.Common.EventLogSeverity.Information);
        }
    }
}
