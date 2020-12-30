namespace Suftnet.Cos.Web
{
    using Suftnet.Cos.Core;
    using System;   
    using System.Web; 
    using System.Web.Mvc;

    public class MvcApplication : System.Web.HttpApplication
    {
        static bool isFirstRequest = true;
        static object isLock = new object();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();                   
        }

        protected void Application_End()
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs args)
        {
            if (isFirstRequest)
            {
                lock (isLock)
                {
                    if (isFirstRequest)
                    {
                        AppStartup.Init(Context);
                        isFirstRequest = false;
                    }
                }
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            GeneralConfiguration.Configuration.Logger.LogError(Server.GetLastError());
            Response.Clear();
            Server.ClearError();

            HttpContext.Current.Server.ClearError();
            HttpContext.Current.Response.Redirect("~/Error", true);
        }

        protected void Application_OnPostAuthenticateRequest(object sender, EventArgs args)
        {
            AuthenticatPath.AuthenticateRequest(this.Context);
        }
    }
}