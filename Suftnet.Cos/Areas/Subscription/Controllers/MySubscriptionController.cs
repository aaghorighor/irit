namespace Suftnet.Cos.Subscription.Controllers
{
    using Common;
    using Service;
    using Suftnet.Cos.CommonController.Controllers;

    using Suftnet.Cos.Core;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Stripe;
  
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Web;
    using Web.Command;
  
    [AuthorizeActionFilter(Constant.BackOfficeOnly)]
    public class MySubscriptionController : BaseController
    {
        #region Resolving dependencies     
                
        private readonly ITenant _tenant;
        private readonly IFactoryCommand _factoryCommand;
        public MySubscriptionController(
            ITenant tenant, IFactoryCommand factoryCommand)
        {
            _tenant = tenant;        
            _factoryCommand = factoryCommand;
        }
      
        #endregion
                   
        [HttpPost]
        public ActionResult Create(string StripeToken, string planTypeId)
        {
            Ensure.NotNullOrEmpty(StripeToken);
            Ensure.NotNullOrEmpty(planTypeId);

            ICustomerProvider _customerProvider = new CustomerProvider(GeneralConfiguration.Configuration.Settings.StripeSecretKey);

            var metaData = new Dictionary<string, string>()
            {
                {"tenantId", this.TenantId.ToString()},
                {"tenantName", this.TenantName}
            };

            _customerProvider.Create(this.CreateCustomerEmail(), StripeToken, planTypeId, this.CreateTaxRate(), metaData);
            return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Change(string id)
        {
            Ensure.NotNullOrEmpty(id);

            var tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
            var stripeAdapterModel = new StripeAdapterModel();
            stripeAdapterModel.Tenant = tenant.Get(this.TenantId);
            if (stripeAdapterModel.Tenant == null)
            {

            }
           
            ISubscriptionProvider _subscriptionProvider = new SubscriptionProvider(GeneralConfiguration.Configuration.Settings.StripeSecretKey);
            var subscription = _subscriptionProvider.UpdateSubscription(stripeAdapterModel.Tenant.CustomerStripeId, id, true);

            ChangeSubscription(subscription.CurrentPeriodStart,
                subscription.CurrentPeriodEnd, id, subscription.Id);

            return RedirectToAction("index", "dashboard", new { area = "subscription" });
        }

        [HttpGet]
        public ActionResult cancel()
        {
            var tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
            var stripeAdapterModel = new StripeAdapterModel();
            stripeAdapterModel.Tenant = tenant.Get(this.TenantId);
            if (stripeAdapterModel.Tenant == null)
            {

            }

            return View(stripeAdapterModel);
        }

        [HttpPost]
        public ActionResult Cancel()
        {
            var tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
            var stripeAdapterModel = new StripeAdapterModel();
            stripeAdapterModel.Tenant = tenant.Get(this.TenantId);
            if (stripeAdapterModel.Tenant == null)
            {

            }

            ISubscriptionProvider _subscriptionProvider = new SubscriptionProvider(GeneralConfiguration.Configuration.Settings.StripeSecretKey);
            var cancelAtPeriodEnd = _subscriptionProvider.CancelSubscription(stripeAdapterModel.Tenant.CustomerStripeId, false);

            //var command = _factoryCommand.Create<DeleteTenantCommand>();
            //command.TenantId = this.TenantId;
            //System.Threading.Tasks.Task.Run(() => command.Execute());

            UpdateTenant(cancelAtPeriodEnd, (int)eSubscriptionStatus.Cancelled);

            return RedirectToAction("confirmation", "mysubscription", new { area = "subscription" });
        }
        public ActionResult Confirmation()
        {
            var tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
            var stripeAdapterModel = new StripeAdapterModel();
            stripeAdapterModel.Tenant = tenant.Get(this.TenantId);
            if (stripeAdapterModel.Tenant == null)
            {

            }

            return View(stripeAdapterModel);
        }
        [HttpGet]
        public ActionResult Download()
        {
            var command = _factoryCommand.Create<DownloadCommand>();
            command.TenantId = this.TenantId;
            command.Execute();

            return File(command.Content.ToArray(), "application/zip", this.UserName + ".zip");
        }

        #region private function  
        private void UpdateTenant(DateTime cancelAtPeriodEnd, int statusId)
        {
            var tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
            var model = tenant.Get(this.TenantId);
            if (model == null)
            {

            }
            
            model.StatusId = statusId;           
            model.ExpirationDate = cancelAtPeriodEnd.AddDays(7);                   
           _tenant.UpdateCustomer(model);          
        }
        private void UpdateTenant(string stripeCustomerId, DateTime? startDate, DateTime? endDate, string planTypeId, string subscriptionId)
        {
            var tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
            var model = tenant.Get(this.TenantId);
            if (model == null)
            {

            }

            model.IsExpired = true;
            model.CustomerStripeId = stripeCustomerId;
            model.StartDate = startDate;
            model.ExpirationDate = (DateTime)endDate;
            model.PlanTypeId = planTypeId;
            model.SubscriptionId = subscriptionId;

            model.CreatedDT = DateTime.Now;
            model.CreatedBy = this.UserName;

           _tenant.UpdateCustomer(model);
        }
        private void ChangeSubscription(DateTime? startDate, DateTime? endDate, string planTypeId, string subscriptionId)
        {
            var tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
            var model = tenant.Get(this.TenantId);
            if (model == null)
            {

            }

            model.IsExpired = false;
            model.StartDate = (DateTime)startDate;
            model.ExpirationDate = (DateTime)endDate;
            model.PlanTypeId = planTypeId;
            model.SubscriptionId = subscriptionId;

            model.CreatedDT = DateTime.Now;
            model.CreatedBy = this.UserName;

           _tenant.UpdateCustomer(model);
        }    
        private string CreateCustomerEmail()
        {
            return this.UserName;
        }
        private decimal? CreateTaxRate()
        {
            return GeneralConfiguration.Configuration.Settings.General.TaxRate ?? 0;
        }       
        private string CreateException(Exception ex)
        {
            GeneralConfiguration.Configuration.Logger.LogError(ex);

            return ex.Message;
        }

        #endregion
    }
}

