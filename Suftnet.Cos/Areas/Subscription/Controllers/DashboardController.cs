namespace Suftnet.Cos.Subscription.Controllers
{
    using System.Web.Mvc;

    using Suftnet.Cos.Web;
    using Suftnet.Cos.Stripe;
  
    using CommonController.Controllers;
    using Core;
    using System;
    using Common;
    using Suftnet.Cos.DataAccess;

    [SubscriptionAuthorizeActionFilter(Constant.BackOfficeOnly)]
    public class DashboardController : BaseController
    {         
        public ActionResult Index()
        {
            var tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
            var adapter = new StripeAdapterModel();
            adapter.Settings = GeneralConfiguration.Configuration.Settings.General;
            adapter.Tenant = tenant.Get(this.TenantId);    
            
            if (adapter.Tenant == null)
            {

            }    

            if (!string.IsNullOrEmpty(adapter.Tenant.CustomerStripeId))
            {              
                IInvoiceProvider _invoiceProvider = new InvoiceProvider(GeneralConfiguration.Configuration.Settings.StripeSecretKey);
                ICardProvider _cardProvider = new CardProvider(GeneralConfiguration.Configuration.Settings.StripeSecretKey);
                ISubscriptionProvider _subscriptionProvider = new SubscriptionProvider(GeneralConfiguration.Configuration.Settings.StripeSecretKey);

                adapter.PaymentCards = _cardProvider.GetAll(adapter.Tenant.CustomerStripeId);
                adapter.Subscription = _subscriptionProvider.Get(adapter.Tenant.SubscriptionId.ToString());
                adapter.Invoices = _invoiceProvider.GetCurrent(adapter.Tenant.CustomerStripeId);             
                adapter.Tenant.IsExpired = adapter.Subscription.CurrentPeriodEnd < DateTime.UtcNow.Date ? true : false;             
            }else
            { adapter.Tenant.IsExpired = adapter.Tenant.ExpirationDate.Date < DateTime.UtcNow.Date ? true : false;}               

            return View(adapter);
        }

        public ActionResult Invoices()
        {
            var tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
            var stripeAdapterModel = new StripeAdapterModel();
            stripeAdapterModel.Tenant = tenant.Get(this.TenantId);
            if (stripeAdapterModel.Tenant == null)
            {

            }

            IInvoiceProvider _invoiceProvider = new InvoiceProvider(GeneralConfiguration.Configuration.Settings.StripeSecretKey);
            stripeAdapterModel.Invoices = _invoiceProvider.GetAll(stripeAdapterModel.Tenant.CustomerStripeId);
            stripeAdapterModel.Settings = GeneralConfiguration.Configuration.Settings.General;

            return View(stripeAdapterModel);
        }

        public ActionResult Invoice(string id)
        {
            var tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();          
            var stripeAdapterModel = new StripeAdapterModel();
            stripeAdapterModel.Tenant = tenant.Get(this.TenantId);
            if (stripeAdapterModel.Tenant == null)
            {

            }

            IInvoiceProvider _invoiceProvider = new InvoiceProvider(GeneralConfiguration.Configuration.Settings.StripeSecretKey);
            stripeAdapterModel.Invoice = _invoiceProvider.Get(stripeAdapterModel.Tenant.CustomerStripeId, id);           
            stripeAdapterModel.Settings = GeneralConfiguration.Configuration.Settings.General;
            
            return View(stripeAdapterModel);
        }   

    }
}