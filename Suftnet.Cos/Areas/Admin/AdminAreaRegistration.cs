using System.Web.Mvc;

namespace Suftnet.Cos.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = false;
            context.Routes.AppendTrailingSlash = false;

            context.MapRoute(
                "adminPermission_",
                "admin/permission/entry/{userName}/{userString}",
                new { AreaName = "admin", Controller = "permission", action = "entry", userName = UrlParameter.Optional, userString = UrlParameter.Optional },
                new string[] { "Suftnet.Cos.Admin.Controllers" }
            );

            context.MapRoute(
               "adminMobilePermission_",
               "admin/mobile-permission/entry/{userName}/{userString}",
               new { AreaName = "admin", Controller = "mobilePermission", action = "entry", userName = UrlParameter.Optional, userString = UrlParameter.Optional },
               new string[] { "Suftnet.Cos.Admin.Controllers" }
            );

            context.MapRoute(
               "User_",
                "admin/user/entry/{name}/{queryString}",
               new { AreaName = "admin", Controller = "user", action = "entry", queryString = UrlParameter.Optional, name = UrlParameter.Optional },
               new string[] { "Suftnet.Cos.Admin.Controllers" }
           );

            context.MapRoute(
               "_tenantUsers",
               "admin/user/fetch/{queryString}",
               new { AreaName = "admin", Controller = "user", action = "fetch", queryString = UrlParameter.Optional },
               new string[] { "Suftnet.Cos.Admin.Controllers" }
            );

            context.MapRoute(
             "_tenant",
             "admin/tenants/entry/{name}/{queryString}",
             new { AreaName = "admin", Controller = "tenants", action = "entry", queryString = UrlParameter.Optional, name = UrlParameter.Optional },
            new string[] { "Suftnet.Cos.Admin.Controllers" }
           );

            context.MapRoute(
            "_tenantOvewView",
            "admin/tenants/fetch/{queryString}",
            new { AreaName = "admin", Controller = "tenants", action = "fetch", queryString = UrlParameter.Optional },
            new string[] { "Suftnet.Cos.Admin.Controllers" }
           );

            context.MapRoute(
              "Common_",
              "admin/common/entry/{name}/{Id}",
              new { AreaName = "admin", Controller = "common", action = "entry", Id = UrlParameter.Optional, name = UrlParameter.Optional },
              new { Id = @"\d+" },
              new string[] { "Suftnet.Cos.Admin.Controllers" }
          );

            context.MapRoute(
               "Topic",
               "admin/topic/entry/{subSection}/{Id}",
               new { AreaName = "admin", Controller = "topic", action = "entry", Id = UrlParameter.Optional , subSection = UrlParameter.Optional },
               new { Id = @"\d+" },
               new string[] { "Suftnet.Cos.Admin.Controllers" }
           );

            context.MapRoute(
               "SubTopic",
               "admin/subtopic/entry/{sectionId}/{subSection}/{topic}/{Id}",
               new { AreaName = "admin", Controller = "subtopic", action = "entry", Id = UrlParameter.Optional, topic = UrlParameter.Optional, sectionId = UrlParameter.Optional , subSection = UrlParameter.Optional },
               new { Id = @"\d+" },
               new string[] { "Suftnet.Cos.Admin.Controllers" }
           );            

            context.MapRoute(
               "Admin_",
               "admin/{controller}/entry/{tenantId}",
               new { AreaName = "admin", Controller = "user", action = "Entry", tenantId = UrlParameter.Optional },
               new string[] { "Suftnet.Cos.Admin.Controllers" }
           );

            context.MapRoute(
                "Admin_default",
                "admin/{controller}/{action}/{id}",
                new { AreaName = "admin", Controller = "dashboard", action = "index", id = UrlParameter.Optional },
                new string[] { "Suftnet.Cos.admin.Controllers" }
            );
        }
    }
}
