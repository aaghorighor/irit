namespace Suftnet.Cos.Admin.Controllers
{
    using ImageResizer;

    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;

    public class TemplateController : AdminBaseController
    {
        #region Resolving dependencies
      
        private readonly IEditor _editor;

        public TemplateController(IEditor editor)
        {    
            _editor = editor;           

        }
        #endregion

        public ActionResult Index()
        {
            return View(_editor.GetAll());
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            try
            {
                return Json(new { ok = true, dataobject = _editor.Get(id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult Create(EditorDTO entityToCreate)
        {
            try
            {                   
                    entityToCreate.CreatedBy = this.UserName;
                    entityToCreate.CreatedDT = DateTime.Now;

                if (entityToCreate.Id == 0)
                {
                    entityToCreate.Id = _editor.Insert(entityToCreate);
                    entityToCreate.flag = (int)flag.Add;
                }
                else
                {
                    _editor.Update(entityToCreate);
                    entityToCreate.flag = (int)flag.Update;
                }              

                return Json(new { ok = true, flag = entityToCreate.flag, objrow = _editor.Get(entityToCreate.Id) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonResult Delete(int Id)
        {
            try
            {
                var editor = _editor.Get(Id);

                if (!string.IsNullOrEmpty(editor.ImageUrl))
                {
                    DeleteFile(editor.ImageUrl);
                }

                return Json(new { ok = _editor.Delete(Id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }   
        public JsonResult Upload(HttpPostedFileBase file)
        {
            try
            {
                var versions = GetVersions();

                string uploadFolder = System.Web.HttpContext.Current.Server.MapPath("~/Content/Photo/cms");

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

                return Json(new { ok = true, FileName = imageUrl, FilePath = "/Content/Photo/cms/_1921x987/" + imageUrl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, FileName = string.Empty, errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        #region private functions
        private void DeleteFile(string filename)
        {
            var versions = GetVersions();

            string uploadFolder = System.Web.HttpContext.Current.Server.MapPath("~/Content/Photo/cms/");

            foreach (string suffix in versions.Keys)
            {
                string filePath = Path.Combine(uploadFolder + "\\" + suffix, filename);

                if (!System.IO.File.Exists(filePath))
                {
                    continue;
                }

                new List<string>(Directory.GetFiles(uploadFolder + suffix)).ForEach(files =>
                {
                    if (files.IndexOf(filename, StringComparison.OrdinalIgnoreCase) >= 0)
                        System.IO.File.Delete(files);
                });
            }
        }
        private Dictionary<string, string> GetVersions()
        {
            var versions = new Dictionary<string, string>();

            versions.Add("_1921x987", "width=1921&height=987&crop=auto&format=jpg");           

            return versions;
        }

        #endregion
    }
}
