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
              "CART_",
              "front-office/cart/entry/{orderId}/{orderTypeId}/{orderType}",
              new { AreaName = "FrontOffice", Controller = "cart", action = "entry", orderId = UrlParameter.Optional, orderTypeId = UrlParameter.Optional, orderType = UrlParameter.Optional },             
              new string[] { "Suftnet.Cos.FrontOffice" }
            );

            context.MapRoute(
              "MENU_BY_CATEGORY_",
              "front-office/cart/fetchMenuByCategory/{categoryId}",
              new { AreaName = "FrontOffice", Controller = "cart", action = "fetchMenuByCategory", categoryId = UrlParameter.Optional },
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
