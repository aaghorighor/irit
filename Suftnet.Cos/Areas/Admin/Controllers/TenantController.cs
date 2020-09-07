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
           
        public async Task<ActionResult> Index()
        {
            var model = await System.Threading.Tasks.Task.Run(() => _tenant.GetAll());
            return View(model);
        }     

    }
}

