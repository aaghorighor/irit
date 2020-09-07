namespace Suftnet.Cos.Web
{
    using System;
    using StructureMap;
    using System.Web.Mvc;
    using Suftnet.Cos.Service;
    using System.Web.Http;

    public class AppStartup
    {
        public static bool m_IsInitialized = false;

        internal static void Init(System.Web.HttpContext httpContext)
        {
            try
            {              
                var httpContextBase = new System.Web.HttpContextWrapper(httpContext);

                Register(httpContextBase, httpContext);
            }
            catch (Exception ex)
            {               
                throw ex;                
            }
        }

        internal static void Register(System.Web.HttpContextBase httpContextBase, System.Web.HttpContext httpContext)
        {
            if (m_IsInitialized)
            {
                return;
            }           
                                             
            IContainer container = StructureMapConfig.GetConfiguredContainer(httpContextBase);

            GlobalConfiguration.Configure(WebApiConfig.Register);
            WebApiActivator.Start(container, GlobalConfiguration.Configuration);
            WebActivator.Start(container, httpContextBase);                  
            SiteConfig.Configure(httpContextBase);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);          

            m_IsInitialized = true;
        }
    }
}