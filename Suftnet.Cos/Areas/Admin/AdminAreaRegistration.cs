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
               "User_",
               "Admin/User/entry/{tenantId}",
               new { AreaName = "admin", Controller = "User", action = "entry", tenantId = UrlParameter.Optional },
               new { userId = @"\d+" },
               new string[] { "Suftnet.Cos.Admin.Controllers" }
           );

            context.MapRoute(
              "Common_",
              "admin/common/entry/{name}/{Id}",
              new { AreaName = "admin", Controller = "Common", action = "entry", Id = UrlParameter.Optional, name = UrlParameter.Optional },
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
                "Permission__",
                "admin/permission/entry/{name}/{queryString}",
                new { AreaName = "admin", Controller = "permission", action = "entry", name = UrlParameter.Optional, queryString = UrlParameter.Optional },
             new string[] { "Suftnet.Cos.Admin.Controllers" }
            );

            context.MapRoute(
               "Admin_",
               "admin/{controller}/entry/{tenantId}",
               new { AreaName = "Admin", Controller = "User", action = "Entry", tenantId = UrlParameter.Optional },
               new string[] { "Suftnet.Cos.Admin.Controllers" }
           );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { AreaName = "Admin", Controller = "Dashboard", action = "Index", id = UrlParameter.Optional },
                new string[] { "Suftnet.Cos.Admin.Controllers" }
            );
        }
    }
}
