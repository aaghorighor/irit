namespace Suftnet.Cos.Web.Infrastructure.Captcha
{
    using Suftnet.Cos.Core;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Script.Serialization;

    public class ReCaptchaService
    {
        private static readonly Uri BaseUri = new Uri("https://www.google.com/recaptcha/api/siteverify");
        private readonly string secret;

        public static HttpClient HttpClient { get; set; } = new HttpClient
        {
            BaseAddress = BaseUri,
            Timeout = TimeSpan.FromSeconds(60)
        };

        public ReCaptchaService(string secret)
        {
            this.secret = secret;
        }

        public async Task<ReCaptchaResponse> VerifyAsync(string responseToken, string remoteIp, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(responseToken))
            {
                return new ReCaptchaResponse
                {
                    Success = false,
                    ResponseStatus = ReCaptchaResponseStatus.Failed,        
                    ErrorCodes = new ReCaptchaErrorCode[] { ReCaptchaErrorCode.MissingInputSecret}
                };
            }            

            var formDictionary = new Dictionary<string, string>
            {
                { "response", responseToken },
                { "secret", secret }
            };

            if (!string.IsNullOrEmpty(remoteIp))
            {
                formDictionary["remoteip"] = remoteIp;
            }

            var form = new FormUrlEncodedContent(formDictionary);
            var response = await HttpClient.PostAsync(BaseUri, form, cancellationToken).ConfigureAwait(false);
            try
            {
                var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                JavaScriptSerializer js = new JavaScriptSerializer();
                return js.Deserialize<ReCaptchaResponse>(responseJson);
            }
            catch(Exception exception)
            {
                GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>().LogError(exception);

                return new ReCaptchaResponse
                {
                    Success = false,
                    ResponseStatus = ReCaptchaResponseStatus.Failed,
                    StatusCode = response.StatusCode,
                    ErrorCodes = new ReCaptchaErrorCode[] { ReCaptchaErrorCode.BadRequest }
                };
            }
        }
    }
}