namespace Suftnet.Cos.Web.Command
{
    using System;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Extension;
    using System.Net.Http;
    using Newtonsoft.Json;
    using System.Text;   
    using Core;
    using System.Collections.Generic;
    using System.Threading;

    public class PushNotificationCommand : ICommand
    {
        private readonly IDevice _device;

        public PushNotificationCommand(IDevice device)
        {
            _device = device;
        }
               
        public string Title { get; set; }
        public string Body { get; set; }
        public string Id { get; set; }
        public string ClickAction { get; set; }
        public Guid TenantId { get; set; }

        public void Execute()
        {
            this.NotifyAsync();
        }

        #region private function
        public void NotifyAsync()
        {
            
            var devices = PrepareDeviceId();
            int milliseconds = 2000;

            foreach (var deviceId in devices)
            {
                var messages = PrepareNotification(deviceId.DeviceId);
                Send(messages);
                Thread.Sleep(milliseconds);
            }          
       
        }

        private string PrepareNotification(string toDevice)
        {           
            var payload = new
            {
                notification = new
                {
                    title = Title,
                    body = Body,                   
                    tag = ClickAction
                },

                data = new
                {
                    click_action = ClickAction,
                    info = ClickAction,
                    Identity = Id
                },
                to = toDevice,
                priority = "high",
                content_available = true
            };

            return JsonConvert.SerializeObject(payload);
        }

        private List<DeviceDto> PrepareDeviceId()
        {
            return _device.GetAll(this.TenantId);
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