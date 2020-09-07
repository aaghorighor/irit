namespace Suftnet.Cos.Subscription.Controllers
{
    using Common;
    using Service;
    using Stripe;

    using Suftnet.Cos.CommonController.Controllers;
    using Suftnet.Cos.Core;
    using Suftnet.Cos.DataAccess;
    using System;
    using System.Web.Mvc; 
    using Web.ViewModel;

    [AuthorizeActionFilter(Constant.BackOfficeOnly)]
    public class PaymentCardController : BaseController
    {
       public ActionResult Index()
       {
           return View();
       }

        public ActionResult Entry(string planTypeId)
        {
            var stripePlanModel = new StripePlanModel
            {
                 Amount = this.CreatePlanPriceType(planTypeId),
                 Total = this.CreatePlanPrice(planTypeId),
                 Vat = this.CreateTaxRate(),               
                 PlanTypeId = planTypeId,
                 Plan = this.CreatePlanName(planTypeId),
                 BillingCycle = this.CreateBillingCycleDescription(planTypeId)
            };

            return View(stripePlanModel);
        }
        public ActionResult Create(string StripeToken)
       {
            try
            {
                Ensure.NotNullOrEmpty(StripeToken);

                var tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
                var model = tenant.Get(this.TenantId);
                if (model == null)
                {

                }
                ICardProvider _cardProvider = new CardProvider(GeneralConfiguration.Configuration.Settings.StripeSecretKey);
                _cardProvider.Create(StripeToken, model.CustomerStripeId);

                return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = true, msg = this.CreateException(ex) }, JsonRequestBehavior.AllowGet);
            }          
        }
        #region private function
        private decimal CreatePlanPriceType(string planTypeId)
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
        private decimal? CreatePlanPrice(string planTypeId)
        {
            var price = this.CreatePlanPriceType(planTypeId);
            var vat = this.CreateTaxRate();
            var total = (price * (vat/100)) + price;

            return Math.Round((decimal)total,2);
        }
        private decimal? CreateTaxRate()
        {
            return Math.Round((decimal)GeneralConfiguration.Configuration.Settings.General.TaxRate,2);
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
                case PlanType.Trial:
                    return PlanNameType.Trial;
            }

            return string.Empty;
        }
        private string CreateException(Exception ex)
        {
            GeneralConfiguration.Configuration.Logger.LogError(ex);

            return ex.Message;
        }
        #endregion
    }
}

