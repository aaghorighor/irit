namespace Suftnet.Cos.Web
{
    using System.Web.Mvc;
   
    public class FeaturesController : MainController
    {       
        [OutputCache(Duration = 10, VaryByParam = "*")]
        public ActionResult Index()
        {
            return View();
        }               
    }
}
