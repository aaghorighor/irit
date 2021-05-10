namespace Suftnet.Cos.Service
{
    using Common;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using StructureMap;

    using Suftnet.Cos.Core;
    using Suftnet.Cos.Filter.AuthHandler;

    using System;
    using System.Linq;
    using System.Web.Http;
    using Web;

    public class WebApiActivator
    {
        public static void Start(IContainer container, HttpConfiguration config)
        {
            GeneralConfiguration.Configuration.DependencyResolver = new StructureMapDependencyResolver(container);
            config.DependencyResolver = new StructureMapDependencyResolver(container);

            //config.MessageHandlers.Add(new LoggerHandler());
            //config.MessageHandlers.Add(new AuthHandler());
                       
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore
            };

           var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

           GeneralConfiguration.Configuration.Logger.Log("Jerur webapi started at :" + DateTime.UtcNow, EventLogSeverity.Information);
        }
    }
}