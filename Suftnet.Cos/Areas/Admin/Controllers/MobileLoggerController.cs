namespace Suftnet.Cos.Admin.Controllers
{  
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Web;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class MobileLoggerController : AdminBaseController
    {
        #region Resolving dependencies        
  
        private readonly IMobileLogger _mobileLogger;

        public MobileLoggerController(IMobileLogger mobileLogger)
        {
            _mobileLogger = mobileLogger;        
        }

        #endregion
           
        public ActionResult Entry()
        {            
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Fetch(DataTableAjaxPostModel param)
        {
            var model = await Task.Run(()=> _mobileLogger.GetAll(param.start, param.length, param.search.value));

            return Json(new
            {
                draw = param.draw,
                recordsTotal = model.MobileLogs.Count,
                recordsFiltered = model.Count,
                data = model.MobileLogs
            },
                      JsonRequestBehavior.AllowGet);
        }
      
        public JsonResult Delete()
        {          
            return Json(new { ok = _mobileLogger.Delete() }, JsonRequestBehavior.AllowGet);
        }
    }
}

