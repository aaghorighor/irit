namespace Suftnet.Cos.Subscription.Controllers
{
    using Common;
    using Microsoft.Owin.Security;
    using Service;
    using Suftnet.Cos.CommonController.Controllers;

    using Suftnet.Cos.Core;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Stripe;
  
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Web;
    using System.Web.Mvc;
    using Web;
    using Web.Command;
  
    [SubscriptionAuthorizeActionFilter(Constant.BackOfficeOnly)]
    public class PaidController : BaseController
    {
        #region Resolving dependencies     
                
        private readonly ITenant _tenant;
        private readonly IFactoryCommand _factoryCommand;
        public PaidController(
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

            var stripeCustomerId = _customerProvider.Create(this.CreateCustomerEmail(), StripeToken, planTypeId, this.CreateTaxRate(), metaData);
            
            UpdateTenant(stripeCustomerId);           

            return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Change(string id)
        {
            Ensure.NotNullOrEmpty(id);

            var tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
            var stripeAdapterModel = new StripeAdapterModel();
            stripeAdapterModel.Tenant = tenant.Get(this.TenantId);
                      
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
            
            return View(stripeAdapterModel);
        }

        [HttpPost]
        public ActionResult Cancel()
        {
            var tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
            var stripeAdapterModel = new StripeAdapterModel();
            stripeAdapterModel.Tenant = tenant.Get(this.TenantId);
            
            ISubscriptionProvider _subscriptionProvider = new SubscriptionProvider(GeneralConfiguration.Configuration.Settings.StripeSecretKey);
            var cancelAtPeriodEnd = _subscriptionProvider.CancelSubscription(stripeAdapterModel.Tenant.CustomerStripeId, true);

            UpdateTenant(cancelAtPeriodEnd, new Guid(SubscriptionStatus.Cancelled));

            return RedirectToAction("index", "dashboard", new { area = "subscription" });
        }
        public ActionResult Confirmation()
        {
            var tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
            var stripeAdapterModel = new StripeAdapterModel();
            stripeAdapterModel.Tenant = tenant.Get(this.TenantId);
           
            return View(stripeAdapterModel);
        }
        //[HttpGet]
        //public ActionResult Download()
        //{
        //    var command = _factoryCommand.Create<DownloadCommand>();
        //    command.TenantId = this.TenantId;
        //    command.Execute();

        //    return File(command.Content.ToArray(), "application/zip", this.UserName + ".zip");
        //}

        #region private function  
        private void UpdateTenant(DateTime cancelAtPeriodEnd, Guid statusId)
        {
            var tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
            var model = tenant.Get(this.TenantId);
                        
            model.StatusId = statusId;           
            model.ExpirationDate = cancelAtPeriodEnd.AddDays(7);    
            
           _tenant.UpdateCustomer(model);          
        }
        private void UpdateTenant(string stripeCustomerId)
        {
            SubscriptionProvider _subscriptionProvider = new SubscriptionProvider(GeneralConfiguration.Configuration.Settings.StripeSecretKey);
            var obj = _subscriptionProvider.GetSubscriptionByCustomerId(stripeCustomerId);
                   
            if (obj != null)
            {
                _tenant.UpdateCustomer(new TenantDto
                {
                    Id = this.TenantId,
                    StartDate = obj.CurrentPeriodStart,
                    IsExpired = false,
                    SubscriptionId = obj.Id,
                    CustomerStripeId = obj.CustomerId,
                    PlanTypeId = obj.Plan.Id,
                    ExpirationDate =(DateTime)obj.CurrentPeriodEnd
                });
            }

            UpdateUserIdentity(obj.CurrentPeriodEnd);
        }
        private void ChangeSubscription(DateTime? startDate, DateTime? endDate, string planTypeId, string subscriptionId)
        {
            var tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
            var model = tenant.Get(this.TenantId);
            
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
            return this.Email;
        }
        private decimal? CreateTaxRate()
        {
            return GeneralConfiguration.Configuration.Settings.General.TaxRate ?? 0;
        }           
        private void UpdateUserIdentity(DateTime? endPeriod)
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            var identity = new ClaimsIdentity(User.Identity);         
            
            identity.RemoveClaim(identity.FindFirst(Identity.ExpirationDate));                     
            identity.AddClaim(new Claim("ExpirationDate", endPeriod.Value.ToString()));

            authenticationManager.AuthenticationResponseGrant =
                new AuthenticationResponseGrant( new ClaimsPrincipal(identity),
                new AuthenticationProperties { IsPersistent = true });
        }       

        #endregion
    }
}

