namespace Suftnet.Cos.Web
{
    using Extension;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class OneChurchRoutes :Controller, IRoutesService
    {
        private string[] m_namespaces = { "Suftnet.Cos.Web" };
        private string[] c_namespaces = { "Suftnet.Cos.Common.Controllers" };
        private string[] s_namespaces = { "Suftnet.Cos.Subscription.Controllers" };

        protected RouteCollection m_Routes;

        public OneChurchRoutes()
        {
            this.m_Routes = RouteTable.Routes;
        }                        

        #region Site Routes

        public const string HOME = "Home";            
        public const string ERROR = "Error";
        public const string LOGIN = "Login";
        public const string LOGOUT = "Logout";
        public const string FORGOTTEN = "Forgotten";
        public const string CONFIRMATION = "Confirmation";
        public const string CREATESUBSCRIPTION = "CreateSubscription";
        public const string CREATESUBSCRIPTIONTRIAL = "CreateSubscriptionTrial";
        public const string SUBSCRIPTION = "Subscription";
        public const string SUBSCRIPTION_TRIAL = "SubscriptionTrial";
        public const string CHECKEMAIL = "CheckEmail";
        public const string CHECKOUTCONFIRMATION = "CheckoutConfirmation";
        public const string PAYMENTCARD = "PaymentCard";
        public const string DIRECTORY = "Directory";
        public const string SUPPORT_DETAILS = "Directory";

        #endregion

        #region Routes

        public virtual void Register()
        {
            SubDomainRoute();
            FrontOfficeRoute();     
            DefaultRoute();
        }      
 
        protected virtual void SubDomainRoute()
        {
           // m_Routes.Add("DomainRoute", new DomainRoute(
           //    "test.localhost:5007",                                     // Domain with parameters
           //    "{action}/{id}",                                        // URL with parameters
           //    new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
           //));
        }

        protected virtual void FrontOfficeRoute()
        {
            m_Routes.LowercaseUrls = true;
            m_Routes.AppendTrailingSlash = true;
            
            m_Routes.MapOneChurchRoute(
              SUPPORT_DETAILS
               , "article/entry/{sectionId}/{supportId}"
               , new { controller = "article", action = "entry", sectionId = UrlParameter.Optional, supportId = UrlParameter.Optional },
                 new { sectionId = @"\d+", supportId = @"\d+" }              
               , m_namespaces
           );

            m_Routes.MapOneChurchRoute(
                 "contact_"
                  , "contact/create/{flag}"
                  , new { controller = "contact", action = "create", flag = UrlParameter.Optional }
                  , m_namespaces
              );

            m_Routes.MapOneChurchRoute(
               CHECKOUTCONFIRMATION
                , "subscription confirmation".ToFriendlyUrl()
                , new { controller = "checkout", action = "Confirmation" }
                , m_namespaces
            );           

            m_Routes.MapOneChurchRoute(
                CHECKEMAIL
                 , "checkout/{email}"
                 , new { controller = "checkout", action = "CheckForCustomerEmail", email = UrlParameter.Optional }
                 , m_namespaces
             );                      

            m_Routes.MapOneChurchRoute(
               SUBSCRIPTION
                  , "checkout/entry/{planId}/{planTypeId}"
                  , new { controller = "checkout", action = "entry", planId = UrlParameter.Optional, planTypeId = UrlParameter.Optional }
                  , m_namespaces
              );

            m_Routes.MapOneChurchRoute(
                CREATESUBSCRIPTION
                 , "create subscription".FriendlyUrl()
                 , new { controller = "checkout", action = "Create" }
                 , m_namespaces
             );

            m_Routes.MapOneChurchRoute(
                  CREATESUBSCRIPTIONTRIAL
                   , "create subscription trial".FriendlyUrl()
                   , new { controller = "checkout", action = "CreateTrial" }
                   , m_namespaces
               );


            m_Routes.MapOneChurchRoute(
                  SUBSCRIPTION_TRIAL
                   , "free 15 days trial".FriendlyUrl()
                   , new { controller = "checkout", action = "trial" }
                   , m_namespaces
               );
                      

            m_Routes.MapOneChurchRoute(
                    "Pricing_"
                    , "pricing"
                    , new { controller = "Pricing", action = "index" }
                    , m_namespaces
                );

                m_Routes.MapOneChurchRoute(
                 FORGOTTEN
                  , "forgotten password".FriendlyUrl()
                  , new { controller = "Account", action = "forgotten" }
                  , m_namespaces
              );

                m_Routes.MapOneChurchRoute(
               CONFIRMATION
                , "confirmation"
                , new { controller = "Account", action = "confirmation" }
                , m_namespaces
            );

            m_Routes.MapOneChurchRoute(
             LOGOUT
              , "logout"
              , new { controller = "Account", action = "LogOff" }
              , m_namespaces
          );

            m_Routes.MapOneChurchRoute(
               LOGIN
                , "login"
                , new { controller = "account", action = "login" }
                , m_namespaces
            );           

            m_Routes.MapOneChurchRoute(
                "Error_"
                , "error"
                , new { controller = "Error", action = "index" }
                , m_namespaces
            );
        }

        protected virtual void DefaultRoute()
        {
           m_Routes.LowercaseUrls = false;
           m_Routes.AppendTrailingSlash = false;
           
            m_Routes.MapOneChurchRoute(
                HOME
                , "{controller}/{action}/{id}"
                , new { controller = "Home", action = "Index", id = UrlParameter.Optional }
                , m_namespaces
            );
        }     

        #endregion
    }
}
