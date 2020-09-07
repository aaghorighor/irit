namespace Suftnet.Cos.Subscription.Controllers
{
    using Common;
    using CommonController.Controllers;
    using Core;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Stripe;
    using Suftnet.Cos.Web;

    using System.Web.Mvc;
    using Web.Services.Interface;

    [AuthorizeActionFilter(Constant.BackOfficeOnly)]
    public class PlanController : BaseController
    {     
        private readonly IPlan _plan;

        public PlanController(IPlan plan)
        {
            _plan = plan;           
        }

        public ActionResult Index()
        {
            var tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
            var model = tenant.Get(this.TenantId);
            if (model == null)
            {

            }

            var stripeAdapterModel = new StripeAdapterModel
            {
                PlanFeatureAdapter = _plan.GetPlanFeatures((int)eProduct.OneChurch),
                Tenant = model
            };

            return View(stripeAdapterModel);
        }

        public ActionResult Entry()
        {
            var tenant = GeneralConfiguration.Configuration.DependencyResolver.GetService<ITenant>();
            var model = tenant.Get(this.TenantId);
            if (model == null)
            {

            }
            var stripeAdapterModel = new StripeAdapterModel
            {
                Tenant = model,
                PlanFeatureAdapter = _plan.GetPlanFeatures((int)eProduct.OneChurch)               
            };

            return View(stripeAdapterModel);
        }
    }
}