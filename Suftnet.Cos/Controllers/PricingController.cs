namespace Suftnet.Cos.Web
{
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class PricingController : MainController
    {     

        public PricingController(IPlan plan)
        {
          
        }

        [OutputCache(Duration = 10, VaryByParam = "*")]
        public ActionResult Index()
        {            
            return View();        
        }               
    }
}
