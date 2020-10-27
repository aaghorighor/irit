namespace Suftnet.Cos.Admin.Controllers
{  
    using Suftnet.Cos.DataAccess;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class TenantController : AdminBaseController
    {
        #region Resolving dependencies        
  
        private readonly ITenant _tenant;

        public TenantController(ITenant tenant)
        {
            _tenant = tenant;        
        }

        #endregion
           
        public ActionResult Index()
        {            
            return View();
        }     

    }
}

