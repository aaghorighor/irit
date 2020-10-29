namespace Suftnet.Cos.Admin.Controllers
{
    using Suftnet.Cos.Web.Command;
    using System.Threading.Tasks;
    using System.Web.Mvc;
     
    public class DashboardController : AdminBaseController
    {
        private readonly IAdminDashboardCommand _dashboardCommand;

        public DashboardController(IAdminDashboardCommand dashboardCommand)
        {
            _dashboardCommand = dashboardCommand;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> OverView()
        {
            var model = await Task.Run(() => _dashboardCommand.Execute());
            return Json(new { ok = true, summary = model }, JsonRequestBehavior.AllowGet);
        }
    }
}

