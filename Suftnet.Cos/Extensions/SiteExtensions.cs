namespace Suftnet.Cos.Extension
{   
    using Suftnet.Cos.Web;

    using System.Web.Mvc;

    public static class SiteExtensions
    {       
        public static string HomeLink(this UrlHelper helper)
        {          
            return helper.RouteOneChurchUrl(OneChurchRoutes.HOME);
        }
        public static string LoginHref(this UrlHelper helper)
        {
            return helper.RouteOneChurchUrl(OneChurchRoutes.LOGIN);
        }

        public static string LogoutHref(this UrlHelper helper)
        {
            return helper.RouteOneChurchUrl(OneChurchRoutes.LOGOUT);
        }
        public static string CheckEmailHref(this UrlHelper helper)
        {
            return helper.RouteOneChurchUrl(OneChurchRoutes.CHECKEMAIL);
        }

        public static string CheckoutConfirmationHref(this UrlHelper helper)
        {
            return helper.RouteOneChurchUrl(OneChurchRoutes.CHECKOUTCONFIRMATION);
        }

        public static string CreateSubscriptionHref(this UrlHelper helper)
        {
            return helper.RouteOneChurchUrl(OneChurchRoutes.CREATESUBSCRIPTION);
        }

        public static string CreateSubscriptionTrialHref(this UrlHelper helper)
        {
            return helper.RouteOneChurchUrl(OneChurchRoutes.CREATESUBSCRIPTIONTRIAL);
        }

        public static string TenantHref(this UrlHelper helper, string name, string id)
        {
            return helper.RouteOneChurchUrl(OneChurchRoutes.DIRECTORY, new { name = name.FriendlyUrl(), id = id });
        }

        public static string SubscriptionHref(this UrlHelper helper, int planId, string planTypeId)
        {
            return helper.RouteOneChurchUrl(OneChurchRoutes.SUBSCRIPTION, new { planId = planId, planTypeId = planTypeId });
        }

        public static string TrialHref(this UrlHelper helper)
        {
            return helper.RouteOneChurchUrl(OneChurchRoutes.SUBSCRIPTION_TRIAL);
        }

        public static string PaymentCardHref(this UrlHelper helper, string planTypeId)
        {
            return helper.RouteOneChurchUrl(OneChurchRoutes.PAYMENTCARD, new { planTypeId = planTypeId });
        }

        public static string SupportDetailsHref(this UrlHelper helper, int Id, string title, int area)
        {
            return helper.RouteOneChurchUrl(OneChurchRoutes.SUPPORT_DETAILS, new { Id = Id, title = title, area = area });
        }

        public static string ForgottenHref(this UrlHelper helper)
        {
            return helper.RouteOneChurchUrl(OneChurchRoutes.FORGOTTEN);
        }
    }
}