namespace Suftnet.Cos.Web
{
    using Suftnet.Cos.Core;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Extension;
    using System;
    using System.Web.Mvc;

    using Suftnet.Cos.Web.ViewModel;

    using Command;
    using Common;
    using global::Stripe;
    using Stripe;   
    using Services.Implementation;
    using Microsoft.AspNet.Identity.Owin;
    using System.Web;
    using DataAccess.Identity;
    using System.Threading.Tasks;
    using Microsoft.Owin.Security;
    using Microsoft.AspNet.Identity;
    using Suftnet.Cos.Web.Services;
    using System.Collections.Generic;
    using Suftnet.Cos.Service;
    using Suftnet.Cos.Web.Infrastructure.ActionFilter;

    public class CheckoutController : AccountBaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IPlan _plan;
        private readonly IFactoryCommand _factoryCommand;
      
        public CheckoutController(
            IPlan plan, IFactoryCommand factoryCommand)
        {
            _plan = plan;           
            _factoryCommand = factoryCommand;
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                if (_userManager == null && HttpContext == null)
                {
                    return new ApplicationUserManager(new Microsoft.AspNet.Identity.EntityFramework.UserStore<ApplicationUser>());
                }

                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        [OutputCache(Duration = 10, VaryByParam = "*")]
        public ActionResult Entry(int planId, string planTypeId)
        {
            switch (planTypeId)
            {
                case "1":
                    planTypeId = PlanType.Basic;
                    break;
                case "2":
                    planTypeId = PlanType.Premium;
                    break;
                case "3":
                    planTypeId = PlanType.PremiumPlus;
                    break;
                default:
                    planTypeId = PlanType.Basic;
                    break;

            }
            return View(this.CreatePlanForCheckout(planId, planTypeId));
        }

        [OutputCache(Duration = 10, VaryByParam = "*")]
        public ActionResult Trial()
        {
            var model = new StripePlanModel
            {
                PlanId = (int)ePlan.Trial,
                PlanTypeId = PlanType.Trial,
                Amount = 0,
                Plan = this.CreatePlanName(PlanType.Trial),
                BillingCycle = this.CreateBillingCycleDescription(PlanType.Trial),
                Total = 0,
                Vat = 0
            };

            return View(model);
        }
        public ActionResult Confirmation()
        {
            return View();
        }
        [HttpGet]
        public JsonResult CheckForCustomerEmail(string email)
        {
            var flag = true;
            var command = _factoryCommand.Create<CheckForExtingCustomerCommand>();
            command.UserName = email;
            command.Execute();

            if (!command.IsCustomerNew)
            {
                flag = false;
            }

            return Json(new { ok = flag }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateCaptchaAttribute]
        [ValidateAntiForgeryToken()]
        public async Task<JsonResult> Create(CheckoutModel checkoutModel)
        {
            Ensure.Argument.NotNull(checkoutModel);

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    ok = false,
                    isValid = true,
                    errors = ModelState.AjaxErrors()
                });
            }

            var command = _factoryCommand.Create<CheckForExtingCustomerCommand>();
            command.UserName = checkoutModel.Email;
            command.Execute();

            if (command.IsCustomerNew)
            {
                return Json(new
                {
                    ok = false,
                    isValid = true,
                    errors = ModelState.EmailErrors()
                });
            }

            var success = await this.CreateCustomerSubscription(checkoutModel);
            return success == true ? Json(new { ok = true }, JsonRequestBehavior.AllowGet) : Json(new { ok = false }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateCaptchaAttribute]
        [ValidateAntiForgeryToken()]
        public async Task<JsonResult> CreateTrial(CheckoutModel checkoutModel)
        {
            Ensure.Argument.NotNull(checkoutModel);

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    ok = false,
                    isValid = true,
                    errors = ModelState.AjaxErrors()
                });
            }

            var command = _factoryCommand.Create<CheckForExtingCustomerCommand>();
            command.UserName = checkoutModel.Email;
            command.Execute();

            if (command.IsCustomerNew)
            {
                return Json(new
                {
                    ok = false,
                    isValid = true,
                    errors = ModelState.EmailErrors()
                });
            }

            var success = await this.CreateCustomerSubscription(checkoutModel);
            return success == true ? Json(new { ok = true }, JsonRequestBehavior.AllowGet) : Json(new { ok = false, isApplication = true }, JsonRequestBehavior.AllowGet);
        }
        #region private function
      
        private async Task<bool> CreateCustomerSubscription(CheckoutModel checkoutModel)
        {
            var success = false;
            try
            {
                var addressCommand = _factoryCommand.Create<CreateAddressCommand>();
                addressCommand.AddressModel = checkoutModel;
                addressCommand.CreatedBy = checkoutModel.Email;
                addressCommand.Execute();

                var customerCommand = _factoryCommand.Create<CreateTenantCommand>();
                customerCommand.AddressId = addressCommand.AddressId;
                customerCommand.CheckoutModel = checkoutModel;
                customerCommand.CutOff = this.CreatePlanCutOff(checkoutModel.PlanTypeId);
                customerCommand.ExpirationDate = this.CreatePlanExpirationDate(checkoutModel.PlanTypeId);
                customerCommand.StartDate = DateTime.UtcNow;
                customerCommand.StatusId = this.CreatePlanStatus(checkoutModel.PlanTypeId);
                customerCommand.CreatedBy = checkoutModel.Email;
                customerCommand.Execute();

                if (checkoutModel.PlanTypeId != PlanType.Trial)
                {
                    await Task.Run(() => CreateStripeSubscription(checkoutModel, customerCommand.TenantId));
                }

                var _user = CreateUser(checkoutModel, customerCommand.TenantId);

                var permissionCommand = _factoryCommand.Create<CreateUserPermissionCommand>();
                permissionCommand.CreatedBy = checkoutModel.Email;
                permissionCommand.UserId = _user.Id;

                await Task.Run(() => permissionCommand.Execute());

                var sendTrialCommand = _factoryCommand.Create<SendTrialConfirmationCommand>();
                sendTrialCommand.TrialModel = checkoutModel;
                sendTrialCommand.VIEW_PATH = this.Server.MapPath("~/App_Data/Email/subscriptionTrial.html");
                sendTrialCommand.Amount = this.CreatePlanPrice(checkoutModel.PlanTypeId);
                sendTrialCommand.BillingCycle = this.CreateBillingCyle(checkoutModel.PlanTypeId);
                sendTrialCommand.Plan = this.CreatePlanName(checkoutModel.PlanTypeId);

                if (checkoutModel.PlanTypeId == PlanType.Trial)
                {
                    await Task.Run(() => sendTrialCommand.Execute());
                }

                await SignInAsync(_user, true);

                success = true;

            }
            catch (StripeException ex)
            {
                var logger = GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>();
                logger.LogError(ex);
                success = false;
            }
            catch (Exception ex)
            {

                var logger = GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>();
                logger.LogError(ex);
                success = false;
            }

            return success;
        }
        private decimal CreatePlanPrice(string planTypeId)
        {
            switch (planTypeId)
            {
                case PlanType.Basic:
                    return PlanPrice.Basic;
                case PlanType.Premium:
                    return PlanPrice.Premium;
                case PlanType.PremiumPlus:
                    return PlanPrice.PremiumPlus;
                case PlanType.Trial:
                    return PlanPrice.Trial;
            }

            return 0;
        }
        private void CreateStripeSubscription(CheckoutModel checkoutModel, Guid tenantId)
        {
            ICustomerProvider _customerProvider = new CustomerProvider(GeneralConfiguration.Configuration.Settings.StripeSecretKey);

            var metaData = new Dictionary<string, string>()
            {
                {"tenantId", tenantId.ToString()},
                {"tenantName",checkoutModel.Name},
                {"app_code", checkoutModel.AppCode}
            };

            var stripeCustomerId = _customerProvider.Create(checkoutModel.Email, checkoutModel.StripeToken,
                    checkoutModel.PlanTypeId, this.CreateTaxRate(), metaData);
            UpdateTenant(stripeCustomerId, tenantId);
        }
        private StripePlanModel CreatePlanForCheckout(int planId, string planTypeId)
        {
            var taxRate = GeneralConfiguration.Configuration.Settings.General?.TaxRate;
            var price = this.CreatePlanPrice(planTypeId);

            var vat = taxRate != null ? price * (taxRate / 100) : 1;

            var stripePlanModel = new StripePlanModel
            {
                PlanId = planId,
                PlanTypeId = planTypeId,
                Amount = Math.Round((decimal)price, 2),
                Total = Math.Round((decimal)(vat + price), 2),
                Vat = Math.Round((decimal)vat, 2),
                Plan = this.CreatePlanName(planTypeId),
                BillingCycle = this.CreateBillingCycleDescription(planTypeId)
            };

            return stripePlanModel;
        }
        private int CreatePlanCutOff(string planTypeId)
        {
            switch (planTypeId)
            {
                case PlanType.Basic:
                    return CutOff.Basic;
                case PlanType.Premium:
                    return CutOff.Premium;
                case PlanType.PremiumPlus:
                    return CutOff.PremiumPlus;
                case PlanType.Trial:
                    return CutOff.Trial;
            }

            return 0;
        }
        private Guid CreatePlanStatus(string planTypeId)
        {
            switch (planTypeId)
            {
                case PlanType.Basic:
                case PlanType.Premium:
                case PlanType.PremiumPlus:
                    return new Guid(SubscriptionStatus.Active);
                case PlanType.Trial:
                    return new Guid(SubscriptionStatus.Trial);
            }

            return Guid.NewGuid();
        }
        private int CreateBillingCyle(string planTypeId)
        {
            switch (planTypeId)
            {
                case PlanType.Basic:
                    return SubscriptionBillingCycleType.Basic;
                case PlanType.Premium:
                    return SubscriptionBillingCycleType.Premium;
                case PlanType.PremiumPlus:
                    return SubscriptionBillingCycleType.PremiumPlus;
                case PlanType.Trial:
                    return SubscriptionBillingCycleType.TrialDays;
            }

            return 0;
        }
        private string CreateBillingCycleDescription(string planTypeId)
        {
            switch (planTypeId)
            {
                case PlanType.Basic:
                    return "Monthly";
                case PlanType.Premium:
                    return "Every 6 Months";
                case PlanType.PremiumPlus:
                    return "Yearly";
                case PlanType.Trial:
                    return "15 days";
            }

            return string.Empty;
        }
        private string CreatePlanName(string planTypeId)
        {
            switch (planTypeId)
            {
                case PlanType.Basic:
                    return PlanNameType.Basic;
                case PlanType.Premium:
                    return PlanNameType.Premium;
                case PlanType.PremiumPlus:
                    return PlanNameType.PremiumPlus;
                default:
                    return PlanNameType.Trial;
            }
        }        
        private DateTime CreatePlanExpirationDate(string planTypeId)
        {
            switch (planTypeId)
            {
                case PlanType.Basic:
                    return DateTime.UtcNow.AddDays(SubscriptionBillingCycleType.Basic);
                case PlanType.Premium:
                    return DateTime.UtcNow.AddDays(SubscriptionBillingCycleType.Premium);
                case PlanType.PremiumPlus:
                    return DateTime.UtcNow.AddDays(SubscriptionBillingCycleType.PremiumPlus);
                case PlanType.Trial:
                    return DateTime.UtcNow.AddDays(SubscriptionBillingCycleType.TrialDays);
            }

            return DateTime.UtcNow;
        }
        private decimal? CreateTaxRate()
        {
            var taxRate = GeneralConfiguration.Configuration.Settings.General.TaxRate;
            if (taxRate == null)
            {
                taxRate = 1;
            }
            return taxRate;
        }
        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await SignInManager.CreateUserIdentityAsync(user);
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }
        private ApplicationUser CreateUser(CheckoutModel userModel, Guid tenantId)
        {
            var apiUserManger = GeneralConfiguration.Configuration.DependencyResolver.GetService<IApiUserManger>();
            var path= this.Server.MapPath("~/App_Data/Email/userRegistration.html");
            var user = apiUserManger.CreateAsync(UserManager, path, userModel.AppCode, new ApplicationUser { PhoneNumber = userModel.Mobile, UserName = userModel.Email, Email = userModel .Email, FirstName = userModel.FirstName, LastName = userModel.LastName }, tenantId, userModel.Password, false, true);

            return user;
        }       
        private void UpdateTenant(string stripeCustomerId, Guid tenantId)
        {
            var _tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
            SubscriptionProvider _subscriptionProvider = new SubscriptionProvider(GeneralConfiguration.Configuration.Settings.StripeSecretKey);
            var obj = _subscriptionProvider.GetSubscriptionByCustomerId(stripeCustomerId);

            if (obj != null)
            {
                _tenant.UpdateCustomer(new TenantDto
                {
                    Id = tenantId,
                    StartDate = obj.CurrentPeriodStart,
                    IsExpired = false,
                    SubscriptionId = obj.Id,
                    CustomerStripeId = obj.CustomerId,
                    PlanTypeId = obj.Plan.Id,
                    ExpirationDate =(DateTime)obj.CurrentPeriodEnd
                });
            }
        }

        #endregion
    }
}
