namespace Suftnet.Cos.Subscription
{
    using System.Web.Mvc;

    public class SubscriptionAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Subscription";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {            
            context.MapRoute(
                "Subscription_",
                "subscription/paymentcard/entry/{planTypeId}",
                new { controller = "PaymentCard", action = "Entry", planTypeId = UrlParameter.Optional },
                new string[] { "Suftnet.Cos.Subscription.Controllers" }
            );

            context.MapRoute(
                "Subscription_default",
                "subscription/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "Suftnet.Cos.Subscription.Controllers" }
            );
        }
    }
}
