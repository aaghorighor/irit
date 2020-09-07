namespace Suftnet.Cos.BackOffice
{
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class TaxController : TenantCommonController
    {            
         private readonly ITenantCommon iCommon;

         public TaxController(ITenantCommon iCommon)
            : base(iCommon)
        {
            this.iCommon = iCommon;
        }           

        public async Task<ActionResult> Index()
        {
            var model = await System.Threading.Tasks.Task.Run(() => iCommon.GetAll((int)eSettings.Tax));
            return View(model);           
        }
    }
}