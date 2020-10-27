namespace Suftnet.Cos.FrontOffice
{
    using System.Web.Mvc;
    using Suftnet.Cos.DataAccess;
    using System.Threading.Tasks;

    public class TableController : FrontOfficeBaseController
    {     
        #region Resolving dependencies

        private readonly ITable _table;     

        public TableController(ITable table)
        {
            _table = table;             
        }
        #endregion
        [OutputCache(Duration = 0, VaryByParam = "*")]
        public ActionResult Index()
        {           
            return View();         
        }

        [HttpGet]
        public async Task<JsonResult> Fetch()
        {           
            return Json(new { ok = true, dataobject = await Task.Run(() => _table.GetAll(this.TenantId)) }, JsonRequestBehavior.AllowGet);
        }
    }
}

