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
    using Cos.Services;
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

    public class CheckoutController : AccountBaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly IPlan _plan;
        private readonly IFactoryCommand _factoryCommand;
        private readonly ISms _messenger;
        private readonly IEditor _editor;
            
        public CheckoutController(
            IPlan plan, IFactoryCommand factoryCommand, ISms messenger, IEditor editor)
        {
            _plan = plan;
            _messenger = messenger;
            _editor = editor;                 
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
        public ActionResult Trial(int planId, string planTypeId)
        {          
            var model = new StripePlanModel
            {
                PlanId = (int)ePlan.Trial,
                PlanTypeId = PlanType.Trial,
                Amount = 0,
                Plan = this.CreatePlanName(planTypeId),
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
        [ValidateAntiForgeryToken()]    
        public JsonResult Create(CheckoutModel checkoutModel)
        {
            try
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

                this.CreateCustomerSubscription(checkoutModel);

                return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
            }
            catch (StripeException ex)
            {
                return Json(new { ok = true, msg = this.CreateException(ex) }, JsonRequestBehavior.AllowGet);                   
            }           
        }      
        [HttpPost]
        [ValidateAntiForgeryToken()]    
        public  JsonResult CreateTrial(CheckoutModel checkoutModel)
        {
            try
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

                this.CreateCustomerSubscription(checkoutModel);

                return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
            }
            catch (StripeException ex)
            {
                return Json(new { ok = true, msg = this.CreateException(ex) }, JsonRequestBehavior.AllowGet);
            }
        }
        #region private function
      
        private decimal? CreatePlanPrice(int planId, string planType)
        {
            var plan = _plan.Get(planId);

            if (plan != null)
            {
                switch(planType)
                {
                    case PlanType.Basic:
                        return plan.BasicPrice;
                    case PlanType.Premium:
                        return plan.AdvancePrice;
                    case PlanType.PremiumPlus:
                        return plan.ProfessionalPrice;
                    default:
                        return 0;
                }           
            }

            return 0;

        }    
        private async void CreateCustomerSubscription(CheckoutModel checkoutModel)
        {            
            var addressCommand = _factoryCommand.Create<CreateAddressCommand>();
            addressCommand.AddressModel = checkoutModel;
            addressCommand.CreatedBy = checkoutModel.Email;
            addressCommand.Execute();

            var customerCommand = _factoryCommand.Create<CreateTenantCommand>();
            customerCommand.AddressId = addressCommand.AddressId;     
            customerCommand.TenantModel = checkoutModel;
            customerCommand.CutOff = this.CreatePlanCutOff(checkoutModel.PlanTypeId);
            customerCommand.ExpirationDate = this.CreatePlanExpirationDate(checkoutModel.PlanTypeId);
            customerCommand.StartDate = DateTime.UtcNow;
            customerCommand.StatusId = this.CreatePlanStatus(checkoutModel.PlanTypeId);
            customerCommand.CreatedBy = checkoutModel.Email;                                  
            customerCommand.Execute();

            if(checkoutModel.PlanTypeId != PlanType.Trial)
            {
                await Task.Run(() => CreateStripeSubscription(checkoutModel, customerCommand.TenantId));
            }          

            var _user= await CreateUser(checkoutModel, customerCommand.TenantId);
          
            var permissionCommand = _factoryCommand.Create<CreateUserPermissionCommand>();
            permissionCommand.CreatedBy = checkoutModel.Email;
            permissionCommand.UserId = _user.Id;           
          
            await Task.Run(() => permissionCommand.Execute());
                       
            var sendSubscriptionCommand = _factoryCommand.Create<SendSubscriptionConfirmationCommand>();
            sendSubscriptionCommand.SubscriptionModel = checkoutModel;
            sendSubscriptionCommand.Amount = this.CreatePlanRateType(checkoutModel.PlanTypeId);
            sendSubscriptionCommand.BillingCycle = this.CreateBillingCyle(checkoutModel.PlanTypeId);
            sendSubscriptionCommand.Plan = this.CreatePlanName(checkoutModel.PlanTypeId);         
         
            await Task.Run(() => sendSubscriptionCommand.Execute());
                       
            await SignInAsync(_user, true);
        }
        private void CreateStripeSubscription(CheckoutModel checkoutModel, Guid tenantId)
        {
            ICustomerProvider _customerProvider = new CustomerProvider(GeneralConfiguration.Configuration.Settings.StripeSecretKey);

            var metaData = new Dictionary<string, string>()
            {
                {"tenantId", tenantId.ToString()},
                {"tenantName",checkoutModel.Name}
            };

            _customerProvider.Create(checkoutModel.Email, checkoutModel.StripeToken,
                   checkoutModel.PlanTypeId, this.CreateTaxRate(), metaData);
        }
        private StripePlanModel CreatePlanForCheckout(int planId, string planTypeId)
        {
            var taxRate = GeneralConfiguration.Configuration.Settings.General?.TaxRate;
            var planPrice = this.CreatePlanPrice(planId, planTypeId);

            if (planPrice != 0)
            {
                var vat = taxRate != null ? planPrice * (taxRate / 100) : planPrice;

                var stripePlanModel = new StripePlanModel
                {
                    PlanId = planId,
                    PlanTypeId = planTypeId,
                    Amount = Math.Round((decimal)planPrice, 2),
                    Total = Math.Round((decimal)(vat + planPrice), 2),
                    Vat = Math.Round((decimal)vat, 2),
                    Plan = this.CreatePlanName(planTypeId),
                    BillingCycle = this.CreateBillingCycleDescription(planTypeId)
                };

                return stripePlanModel;
            }

            return new StripePlanModel();
        }
        private int CreatePlanCutOff(string planTypeId)
        {
            switch(planTypeId)
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

            return new Guid();
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
        private string CreatePassword(int count = 10)
        {          
            return this.RandomPassword();
        }
        private decimal CreatePlanRateType(string planTypeId)
        {
            switch (planTypeId)
            {
                case PlanType.Basic:
                    return PlanRateType.Basic;
                case PlanType.Premium:
                    return PlanRateType.Premium;
                case PlanType.PremiumPlus:
                    return PlanRateType.PremiumPlus;
                case PlanType.Trial:
                    return PlanRateType.Trial;
            }

            return 0;
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
            return GeneralConfiguration.Configuration.Settings.General.TaxRate ?? 1;
        }
        private string CreateException(Exception ex)
        {
            GeneralConfiguration.Configuration.Logger.LogError(ex);
            return ex.Message;
        }       
        private string CreateEditor(int Id)
        {
            var editor = _editor.Get(Id);
            return editor.Contents;
        }
        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {           
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await SignInManager.CreateUserIdentityAsync(user);
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);          
        }
        private SignInStatus StatusCode(SignInStatus statusId)
        {
            switch (statusId)
            {
                case SignInStatus.Success:
                    return SignInStatus.Success;
                case SignInStatus.LockedOut:
                    return SignInStatus.LockedOut;
                case SignInStatus.RequiresVerification:
                    return SignInStatus.RequiresVerification;
                case SignInStatus.Failure:
                    return SignInStatus.Failure;
                default:
                    return SignInStatus.Failure;
            }
        }
        private async Task<ApplicationUser> CreateUser(CheckoutModel model, Guid tenantId)
        {
            var apiUserManger = GeneralConfiguration.Configuration.DependencyResolver.GetService<IApiUserManger>();

            var applicationUser = new ApplicationUser
            {               
                FirstName = model.FirstName,
                LastName = model.LastName,
                Active = true,
                Email = model.Email,
                PhoneNumber = model.Mobile,
                UserName = model.Email       
            };

            var user = await apiUserManger.CreateAsync(applicationUser, tenantId, model.Password, false, true);

            return user;
        }
       
        
        #endregion
    }
}
