namespace Suftnet.Cos.BackOffice
{
    using Suftnet.Cos.Web.Command;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class DashboardController : BackOfficeBaseController
    {
        private readonly IDashboardCommand _dashboardCommand;

        public DashboardController(IDashboardCommand dashboardCommand)
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
            _dashboardCommand.TenantId = this.TenantId;
            var model = await Task.Run(() =>  _dashboardCommand.Execute() );
            return Json(new { ok = true, summary = model }, JsonRequestBehavior.AllowGet);
        }
    }
}
