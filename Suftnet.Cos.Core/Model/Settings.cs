namespace Suftnet.Cos.Core
{
    using Suftnet.Cos.DataAccess;

    public class Settings
    {
        public Settings()
        {
            FormatSettings = new FormatSettings();          
            General = new GlobalDto(); 
        }

        public string SiteName { get; set; }        
        public string ApplicationName { get; set; }      
        public byte[] Version { get; set; }
        public string StripeSecretKey { get; set; }
        public string StripePublishableKey { get; set; }
        public string LogoFullUrl { get; set; }      
        public string PhysicalPath { get; set; }    
        public string ProductId { get; set; }
        public string TempPath { get; set; }      
        public string ImagePath { get; set; }       
        public string DocumentPath { get; set; }
        public string MissingProductImageVirtualPath { get; set; }       
        public byte[] CryptoKey { get; set; }       
        public byte[] CryptoIV { get; set; }        
        public string CurrentUrl { get; set; }       
        public string DefaultUrl { get; set; }       
        public string ServiceUrl { get; set; }
        public string SendGridApi { get; set; }
        public string TwilioAccountSid { get; set; }
        public string TwilioAuthToken { get; set; }
        public string TwilioPhoneNumber { get; set; }
        public string TwilioImageUrl { get; set; }        
        public string CookieDomain { get; set; }
        public string ResponseMessage { get; set; }
        public string RequestMessage { get; set; }
        public string PushNotificationServerkey { get; set; }
        public string PushNotificationSenderId { get; set; }
        public string PushNotificationUrl { get; set; }
        public GlobalDto General { get; set; }
        public FormatSettings FormatSettings { get; set; }
        public string CaptchaSiteKey { get; set; }
        public string CaptchaSecretKey { get; set; }
        public string OnlineLink { get; set; }
        public string MobileLink { get; set; }

    }
}
