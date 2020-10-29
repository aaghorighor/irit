namespace Suftnet.Cos.Admin.Controllers
{
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Web;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class LoggerController : AdminBaseController
    {
        #region Resolving dependencies        
  
        private readonly ILogViewer _logger;

        public LoggerController(ILogViewer logger)
        {
            _logger = logger;        
        }

        #endregion
           
        public ActionResult Entry()
        {            
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Fetch(DataTableAjaxPostModel param)
        {
            var model = await Task.Run(()=> _logger.GetAll(param.start, param.length, param.search.value));

            return Json(new
            {
                draw = param.draw,
                recordsTotal = model.Logs.Count,
                recordsFiltered = model.Count,
                data = model.Logs
            },
                      JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete()
        {
            return Json(new { ok = _logger.Delete() }, JsonRequestBehavior.AllowGet);
        }
    }
}

