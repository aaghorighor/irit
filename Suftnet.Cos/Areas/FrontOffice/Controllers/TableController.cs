namespace Suftnet.Cos.FrontOffice
{
    using System;
    using System.Web.Mvc;

    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.CommonController.Controllers;
    using Suftnet.Cos.Extension;
    using Service;
    using System.Linq;
    using Suftnet.Cos.Web;
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
        [OutputCache(Duration = 10, VaryByParam = "*")]
        public ActionResult Index()
        {           
            return View();         
        }

        [HttpGet]
        public async Task<JsonResult> Fetch()
        {
            var model = await Task.Run(() => _table.GetAll(this.TenantId));
            return Json(new { ok = true, dataobject = model }, JsonRequestBehavior.AllowGet);
        }
    }
}

