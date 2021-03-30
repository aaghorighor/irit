﻿using System.Web.Mvc;

namespace Suftnet.Cos.SiteAdmin
{
    public class SiteAdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "SiteAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {           
            context.MapRoute(
              "Topic_",
              "siteadmin/topic/entry/{subSection}/{Id}",
              new { AreaName = "siteadmin", Controller = "topic", action = "entry", Id = UrlParameter.Optional, subSection = UrlParameter.Optional },
              new { Id = @"\d+" },
              new string[] { "Suftnet.Cos.SiteAdmin.Controllers" }
          );

            context.MapRoute(
               "SubTopic_",
               "siteadmin/subtopic/entry/{sectionId}/{subSection}/{topic}/{Id}",
               new { AreaName = "siteadmin", Controller = "subtopic", action = "entry", Id = UrlParameter.Optional, topic = UrlParameter.Optional, sectionId = UrlParameter.Optional, subSection = UrlParameter.Optional },
               new { Id = @"\d+" },
               new string[] { "Suftnet.Cos.SiteAdmin.Controllers" }
           );
                       
            context.MapRoute(
                "SiteAdmin_default",
                "siteadmin/{controller}/{action}/{id}",
                new { AreaName = "siteadmin", Controller = "dashboard", action = "Index", id = UrlParameter.Optional },
                new string[] { "Suftnet.Cos.SiteAdmin.Controllers" }
            );
        }
    }
}
