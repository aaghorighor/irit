namespace Suftnet.Cos.Admin.Controllers
{
    using Cos.CommonController.Controllers;
    using System.Web.Mvc;
     
    public class DashboardController : AdminBaseController
    {                    
        public ActionResult Index()
        {
            return View();
        }                
    }
}

