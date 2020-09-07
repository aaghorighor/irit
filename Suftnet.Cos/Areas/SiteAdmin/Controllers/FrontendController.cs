namespace Suftnet.Cos.SiteAdmin.Controllers
{   
    using Suftnet.Cos.Common;
    using Suftnet.Cos.CommonController.Controllers;
    using Suftnet.Cos.DataAccess;
    using System.Web.Mvc;

    [AdminAuthorizeActionFilter(Constant.SiteAdminOnly)]
    public class FrontendController : Suftnet.Cos.Admin.Controllers.CommonController
    {
        public FrontendController(ICommon common) : base(common)
        {

        }

        public ActionResult Entry(int id)
        {
            return View();
        }
    }
}
