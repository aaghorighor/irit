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
    using Web.Command;

    public class CommonController : AdminBaseController
    {     
        #region Resolving dependencies

        private readonly ICommon  _common;       
      
        public CommonController(ICommon common)
        {             
            _common = common;                           
        }

        #endregion            

        public ActionResult Entry(string name, int Id)
        {
            return View();
        }

        [HttpGet]
        public JsonResult Get(int Id)
        {
            return Json(new { ok = true, dataobject = _common.Get(Id) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAll(int Id)
        {
            return Json(new { ok = true, dataobject = _common.GetAll(Id) }, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]       
        public JsonResult Create(CommonDto entityToCreate)
        {
            entityToCreate.CreatedDT = DateTime.Now;          
            entityToCreate.CreatedBy = this.UserName;

            if (entityToCreate.Id == 0)
            {
                entityToCreate.Id = _common.Insert(entityToCreate);
                entityToCreate.flag = (int)flag.Add;               
            }
            else
            {
                _common.Update(entityToCreate);
                entityToCreate.flag = (int)flag.Update;                              
            }         

            return Json(new { ok = true, flag = entityToCreate.flag, objrow = _common.Get(entityToCreate.Id) }, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonResult Delete(int Id)
        {
            var common = _common.Get(Id);

            if (!string.IsNullOrEmpty(common.ImageUrl))
            {
                DeleteImage(common.ImageUrl);
            }

            _common.Delete(Id);

            return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Upload(HttpPostedFileBase file)
        {
            try
            {
                var versions = GetVersions();

                //Get the physical path for the uploads folder and make sure it exists
                string uploadFolder = System.Web.HttpContext.Current.Server.MapPath("~/Content/Photo/Common");

                if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                //Generate each version
                var imageUrl = string.Empty;
                var guid = System.Guid.NewGuid().ToString();
                var path = string.Empty;

                foreach (string suffix in versions.Keys)
                {
                    if (!Directory.Exists(uploadFolder + "/" + suffix)) Directory.CreateDirectory(uploadFolder + "/" + suffix);

                    //Generate a filename (GUIDs are best).
                    string fileName = Path.Combine(uploadFolder + "\\" + suffix, guid);

                    //Let the image builder add the correct extension based on the output file type
                    path = ImageBuilder.Current.Build(file, fileName, new ResizeSettings(versions[suffix]), false, true);

                    int index1 = path.LastIndexOf('\\');
                    if (index1 != -1)
                    {
                        imageUrl = path.Substring(index1 + 1);
                    }
                }

                return Json(new { ok = true, FileName = imageUrl, FilePath = "/Content/Photo/Common/_60x60/" + imageUrl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, FileName = string.Empty, errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #region private
        private void DeleteImage(string filename)
        {
            var versions = GetVersions();

            string uploadFolder = System.Web.HttpContext.Current.Server.MapPath("~/Content/Photo/Common/");

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
            Dictionary<string, string> versions = new Dictionary<string, string>();

            //Define the versions to generate           
            versions.Add("_60x60", "width=60&height=60&crop=auto&format=jpg");
            //versions.Add("_630x455", "width=1918&height=967&crop=auto&format=jpg");
            return versions;
        }
        #endregion

    }
}
