namespace Suftnet.Cos.BackOffice
{
    using System.Web.Mvc;

    public class DashboardController : BackOfficeBaseController
    {
        public ActionResult Index()
        {            
            //if(this.Tenant.Startup == false)
            //{
            //    return RedirectToAction("index", "settings", new { area = "backoffice" });
            //}

            return View();
        }       
        
    }
}
