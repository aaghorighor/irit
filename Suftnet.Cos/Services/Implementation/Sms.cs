namespace Suftnet.Cos.Services
{
    using Suftnet.Cos.Core;    
    using Twilio;

    public class Sms : ISms
    {
        private readonly TwilioRestClient _client;       

        public Sms()
        {            
            _client = new TwilioRestClient(GeneralConfiguration.Configuration.Settings.TwilioAccountSid, GeneralConfiguration.Configuration.Settings.TwilioAuthToken);
        }

        public Message SendMessage(string to, string body)
        {                     
            return _client.SendMessage(GeneralConfiguration.Configuration.Settings.TwilioPhoneNumber, to, HtmlToText.ConvertHtml(body));
        }
        public Message SendMessage(string from, string to, string body)
        {
            return _client.SendMessage(from, to, body);
        }

    }
}