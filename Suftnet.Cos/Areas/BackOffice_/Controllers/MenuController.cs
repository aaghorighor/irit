namespace Suftnet.Cos.BackOffice
{
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using System;
    using System.Web.Mvc;
    using System.Linq;
    using Web.ActionFilter;
    using Suftnet.Cos.Extension;
    using System.Threading.Tasks;
    using Web;
    using System.Collections.Generic;
    using System.Web;
    using System.IO;
    using ImageResizer;

    public class MenuController : BackOfficeBaseController
    {      
        #region Resolving dependencies

        private readonly IMenu _menu;

        public MenuController(IMenu menu)
        {
            _menu = menu;                 
        }

        #endregion

        public ActionResult Index()
        {           
            return View();
        }

        public async Task<JsonResult> Init(JQueryDataTableParamModel param)
        {
            IEnumerable<string[]> result = null;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                result = result = await System.Threading.Tasks.Task.Run(() => (from n in _menu.GetAll(param.iDisplayStart, param.iDisplayLength, param.sSearch)
                                                                               orderby n.Id descending
                                                                               select new string[] { n.Id.ToString(), n.Category, n.Name, n.Unit, n.Quantity.ToString(), n.Price.DecimalNullToCurrency(), n.Active == true ? "Yes" : "No", "Actions" }));
            }
            else
            {
                result = result = await System.Threading.Tasks.Task.Run(() => (from n in _menu.GetAll(param.iDisplayStart, param.iDisplayLength)
                                                                               orderby n.Id descending
                                                                               select new string[] { n.Id.ToString(), n.Category, n.Name, n.Unit, n.Quantity.ToString(), n.Price.DecimalNullToCurrency(), n.Active == true ? "Yes" : "No", "Actions" }));
            }

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = result.Count(),
                iTotalDisplayRecords = _menu.Count(),
                aaData = result
            },
              JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> Get(int Id)
        {
            try
            {
                var model = await System.Threading.Tasks.Task.Run(() => _menu.Get(Id));
                return Json(new { ok = true, dataobject = model }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }              
              
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]       
        public JsonResult Create(MenuDto entityToCreate)
        {
            try
            {
                if (entityToCreate == null)
                {
                    return Json(new { ok = false, msg = Constant.ValidationErrorMessage }, JsonRequestBehavior.AllowGet);
                }

                entityToCreate.CreatedBy = this.UserName;
                entityToCreate.CreatedDT = DateTime.UtcNow;
                entityToCreate.TenantId = this.TenantId;

                if (string.IsNullOrEmpty(entityToCreate.ImageUrl))
                {
                    entityToCreate.ImageUrl = "Blank.jpg";
                }

                if (entityToCreate.Id == 0)
                {                  
                    entityToCreate.Id = _menu.Insert(entityToCreate);
                    entityToCreate.flag = (int)flag.Add;                                     
                }
                else
                {
                    _menu.Update(entityToCreate);
                    entityToCreate.flag = (int)flag.Update;
                }

                return Json(new { ok = true, Id = entityToCreate.Id, flag = entityToCreate.flag, objrow = _menu.Get(entityToCreate.Id) }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]       
        public async Task<JsonResult> Delete(int Id)
        {
            try
            {
                var model = await System.Threading.Tasks.Task.Run(() => _menu.Delete(Id));
                return Json(new { ok = model }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        #region private function

        public JsonResult Upload(HttpPostedFileBase file)
        {
            try
            {
                var versions = GetVersions();
                              
                string uploadFolder = System.Web.HttpContext.Current.Server.MapPath("~/Content/Photo/Menu");

                if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);
                             
                var imageUrl = string.Empty;
                var guid = System.Guid.NewGuid().ToString();
                var path = string.Empty;

                foreach (string suffix in versions.Keys)
                {
                    if (!Directory.Exists(uploadFolder + "/" + suffix)) Directory.CreateDirectory(uploadFolder + "/" + suffix);
                                       
                    string fileName = Path.Combine(uploadFolder + "\\" + suffix, guid);
                                 
                    path = ImageBuilder.Current.Build(file, fileName, new ResizeSettings(versions[suffix]), false, true);

                    int index1 = path.LastIndexOf('\\');
                    if (index1 != -1)
                    {
                        imageUrl = path.Substring(index1 + 1);
                    }
                }

                return Json(new { ok = true, FileName = imageUrl, FilePath = "/Content/Photo/Menu/216x196/" + imageUrl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, FileName = string.Empty, errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        private Dictionary<string, string> GetVersions()
        {
            Dictionary<string, string> versions = new Dictionary<string, string>();

            //Define the versions to generate   
         
            versions.Add("216x196", "width=216&height=196&crop=auto&format=jpg");

            return versions;
        }

        #endregion
    }
}

