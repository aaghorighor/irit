namespace Suftnet.Cos.SiteAdmin.Controllers
{
    using Admin.Controllers;
    using Common;
    using Suftnet.Cos.CommonController.Controllers;
    using System.Web.Mvc;

    [AdminAuthorizeActionFilter(Constant.SiteAdminOnly)]
    public class DashboardController : AdminBaseController
    {                    
        public ActionResult Index()
        {
            return View();
        }                
    }
}

