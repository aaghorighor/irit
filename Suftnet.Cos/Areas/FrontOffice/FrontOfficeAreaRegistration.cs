using System.Web.Mvc;

namespace Suftnet.Cos.FrontOffice
{
    public class FrontOfficeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "FrontOffice";
            }
        }        

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            context.Routes.AppendTrailingSlash = false;

            context.MapRoute(
              "AddonType___",
              "front-office/addonType/entry/{name}/{menuId}",
              new { AreaName = "FrontOffice", Controller = "addonType", action = "entry", menuId = UrlParameter.Optional, name = UrlParameter.Optional },             
              new string[] { "Suftnet.Cos.FrontOffice" }
            );               
                      
                        
            context.MapRoute(
                "FrontOffice_default",
                "front-office/{controller}/{action}/{id}",
                new { AreaName = "FrontOffice", Controller = "Dashboard", action = "Index", id = UrlParameter.Optional },
                new string[] { "Suftnet.Cos.FrontOffice" }
            );
        }
    }
}
