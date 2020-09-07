namespace Suftnet.Cos.SiteAdmin.Controllers
{
    using Common;
    using Suftnet.Cos.CommonController.Controllers;
    using Suftnet.Cos.DataAccess;

    using System.Web.Mvc;
    using Web.Command;

    [AdminAuthorizeActionFilter(Constant.SiteAdminOnly)]
    public class PageHeaderController : Suftnet.Cos.Admin.Controllers.CommonController
    {
        private readonly ICommon iCommon;
        public PageHeaderController(ICommon iCommon)
            : base(iCommon)
        {
            this.iCommon = iCommon;
        }               

        public ActionResult Index()
        {
            return View(iCommon.GetAll((int)eSettings.PageHeader));
        }  
    }
}