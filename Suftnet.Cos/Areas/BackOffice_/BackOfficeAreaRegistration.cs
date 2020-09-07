using System.Web.Mvc;

namespace Suftnet.Cos.BackOffice
{
    public class BackOfficeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "BackOffice";
            }
        }        

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            context.Routes.AppendTrailingSlash = false;

            context.MapRoute(
              "AddonType_",
              "back-office/addonType/entry/{menuId}",
              new { AreaName = "BackOffice", Controller = "addonType", action = "entry", menuId = UrlParameter.Optional },
              new { menuId = @"\d+" },
              new string[] { "Suftnet.Cos.BackOffice" }
            );

            context.MapRoute(
              "Addon_",
              "back-office/addon/entry/{menuId}",
              new { AreaName = "BackOffice", Controller = "addon", action = "entry", menuId = UrlParameter.Optional },
              new { menuId = @"\d+" },
              new string[] { "Suftnet.Cos.BackOffice" }
            );

            context.MapRoute(
               "Notification_",
               "back-office/notification/entry/{Id}",
               new { AreaName = "BackOffice", Controller = "notification", action = "entry", Id = UrlParameter.Optional },
               new { Id = @"\d+" },
               new string[] { "Suftnet.Cos.BackOffice" }
            );

            context.MapRoute(
                "Permission_",
                "back-office/permission/entry/{name}/{queryString}",
                new { AreaName = "BackOffice", Controller = "permission", action = "entry", name = UrlParameter.Optional, queryString = UrlParameter.Optional },
                new string[] { "Suftnet.Cos.BackOffice" }
            );

            context.MapRoute(
               "MobilePermission_",
               "back-office/mobilePermission/entry/{name}/{queryString}",
               new { AreaName = "BackOffice", Controller = "mobilePermission", action = "entry", name = UrlParameter.Optional, queryString = UrlParameter.Optional },
               new string[] { "Suftnet.Cos.BackOffice" }
            );          
          
            context.MapRoute(
             "lookup_"
               , "back-office/{controller}/entry/{settingId}/{sectionId}"
               , new { AreaName = "BackOffice", controller = "lookup", action = "entry", settingId = UrlParameter.Optional, sectionId = UrlParameter.Optional },             
                new string[] { "Suftnet.Cos.BackOffice" }
            );                       
                        
            context.MapRoute(
                "BackOffice_default",
                "back-office/{controller}/{action}/{id}",
                new { AreaName = "BackOffice", Controller = "Dashboard", action = "Index", id = UrlParameter.Optional },
                new string[] { "Suftnet.Cos.BackOffice" }
            );
        }
    }
}
