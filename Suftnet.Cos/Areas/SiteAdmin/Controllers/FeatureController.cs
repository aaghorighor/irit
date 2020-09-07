namespace Suftnet.Cos.SiteAdmin.Controllers
{
    using Common; 
    using Suftnet.Cos.CommonController.Controllers;
    using Suftnet.Cos.DataAccess;

    using System.Web.Mvc;
    using Web.Command;

    [AdminAuthorizeActionFilter(Constant.SiteAdminOnly)]
    public class FeatureController : Suftnet.Cos.Admin.Controllers.CommonController
    {
        private readonly ICommon iCommon;
        public FeatureController(ICommon iCommon)
            : base(iCommon)
        {
            this.iCommon = iCommon;
        }               

        public ActionResult Index()
        {
            return View(iCommon.GetAll((int)eSettings.Feature));
        }  
    }
}