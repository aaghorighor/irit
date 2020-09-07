namespace Suftnet.Cos.Web
{  
    using System.Web.Mvc;

    public class HomeController : MainController
    { 
        public ActionResult Index()
        {
            return View();        
        }               
    }
}
