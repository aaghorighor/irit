namespace Suftnet.Cos.SiteAdmin.Controllers
{   
    using Suftnet.Cos.Common;
    using Suftnet.Cos.CommonController.Controllers;
    using Suftnet.Cos.DataAccess;
    using System.Web.Mvc;

    [AdminAuthorizeActionFilter(Constant.SiteAdminOnly)]
    public class BackendController : Suftnet.Cos.Admin.Controllers.CommonController
    {
        public BackendController(ICommon common) : base(common)
        {

        }

        public ActionResult Entry(int id)
        {
            return View();
        }
    }
}
