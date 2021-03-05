namespace Suftnet.Cos.Web.Command
{
    using System;
    using Suftnet.Cos.DataAccess;   
    using System.Net.Http;
    using Newtonsoft.Json;
    using System.Text;   
    using Core;  
   

    public class PushNotificationCommand : ICommand
    {
        private readonly ICustomer _customer;

        public PushNotificationCommand(ICustomer customer)
        {
            _customer = customer;
        }
               
        public string Title { get; set; }
        public string Body { get; set; }
        public string Id { get; set; }
        public string MessageTypeId { get; set; }
        public string OrderStatusId { get; set; }
        public string FcmToken { get; set; }

        public void Execute()
        {
            this.NotifyAsync();
        }

        #region private function
        private void NotifyAsync()
        {                 
            var messages = CreateNotification(FcmToken);
            Send(messages);
        }

        private string CreateNotification(string toDevice)
        {           
            var payload = new
            {
                notification = new
                {
                    title = Title,
                    body = Body          
                },

                data = new
                {
                    title = Title,
                    body = Body,
                    orderStatusId = OrderStatusId,
                    messageTypeId = MessageTypeId
                },
                to = toDevice,
                priority = "high",
                content_available = true
            };

            return JsonConvert.SerializeObject(payload);
        }     

        private async void Send(string messages)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, GeneralConfiguration.Configuration.Settings.PushNotificationUrl);

            httpRequest.Headers.TryAddWithoutValidation("Authorization", string.Format("key={0}", GeneralConfiguration.Configuration.Settings.PushNotificationServerkey));
            httpRequest.Headers.TryAddWithoutValidation("Sender", string.Format("id={0}", GeneralConfiguration.Configuration.Settings.PushNotificationSenderId));
            
            using (var httpClient = new HttpClient())
            {
                httpRequest.Content = new StringContent(messages, Encoding.UTF8, "application/json");

                try
                {
                    var result = await httpClient.SendAsync(httpRequest);

                    if (!result.IsSuccessStatusCode)
                    {
                        GeneralConfiguration.Configuration.Logger.Log(result.ReasonPhrase, Cos.Common.EventLogSeverity.Information);
                    }
                }
                catch (Exception ex)
                {
                    GeneralConfiguration.Configuration.Logger.LogError(ex);
                }
            }
        }

        #endregion

    }
}