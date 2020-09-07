namespace Suftnet.Cos.Admin.Controllers
{   
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Web;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class LogController : AdminBaseController
    {     
        #region Resolving dependencies

        private readonly ILogViewer  _log;      
      
        public LogController(ILogViewer log)
        {
            _log = log;                           
        }

        #endregion      
      
        public ActionResult Index()
        {           
            return View();
        } 

        [HttpGet]
        public JsonResult Get(int Id)
        {
            try
            {
                return Json(new { ok = true, dataobject = _log.Get(Id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        public JsonResult Init(JQueryDataTableParamModel param)
        {
            IEnumerable<string[]> result = null;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                result = (from n in _log.GetAll(param.iDisplayStart, param.iDisplayLength, param.sSearch)
                         orderby n.Id descending
                         select new string[] { n.Id.ToString(), n.CreatedDt.ToString(), n.CreatedBy, n.Description, "Actions" }).ToList();
            }
            else
            {
                result = from n in _log.GetAll(param.iDisplayStart, param.iDisplayLength)
                         orderby n.Id descending
                         select new string[] { n.Id.ToString(), n.CreatedDt.ToString(), n.CreatedBy, n.Description, "Actions" };
            }

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = result.Count(),
                iTotalDisplayRecords = _log.Count(),
                aaData = result
            },
                      JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonResult Delete(int Id)
        {
            try
            {
               bool response = _log.Delete(Id);
               return Json(new { ok = response }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }       
     
    }
}
