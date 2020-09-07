namespace Suftnet.Cos.Service
{
    using Suftnet.Cos.Common;
    using Suftnet.Cos.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;

    public class LoggerHandler : DelegatingHandler
	{
		public LoggerHandler()
		{
			this.Logger = GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>();
		}

		protected ILogger Logger { get; private set; }

		protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
		{
			if (request.Content != null)
			{
				var buffer = request.Content.ReadAsByteArrayAsync().Result;
				var content = System.Text.Encoding.UTF8.GetString(buffer);

                if(request.Headers != null)
                {
                    Dictionary<string, string> ss = request.Headers.ToDictionary(a => a.Key, a => string.Join(";", a.Value));

                    string headers = string.Empty;
                    foreach (var key in ss.Values)
                        headers += key + "=" + key + Environment.NewLine;                 
                }        

                Logger.Log("Post-Request Content:" + content, EventLogSeverity.Information);
			}

			return base.SendAsync(request, cancellationToken);
		}
	}
}
