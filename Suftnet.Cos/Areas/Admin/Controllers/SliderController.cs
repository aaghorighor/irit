namespace Suftnet.Cos.Admin.Controllers
{
    using Common;
    using ImageResizer;

    using Suftnet.Cos.DataAccess;

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;

    public class SliderController : AdminBaseController
    {      
        #region Resolving dependencies

        private readonly ISlider _slider;

        public SliderController(ISlider slider)
        {
            _slider = slider;         
        }

        #endregion
       
        public ActionResult Index()
        {
            return View(_slider.GetAll());
        } 
        
        [HttpGet]
        public JsonResult Get(int Id)
        {
            try
            {
                return Json(new { ok = true, dataobject = _slider.Get(Id) }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }
      
        [HttpPost]
         public JsonResult Create(SliderDto entityToCreate)
        {           
            try
            {
                   entityToCreate.CreatedBy = this.UserName;
                   entityToCreate.CreatedDT = DateTime.UtcNow;                  

                if (entityToCreate.Id == 0)
                {
                    entityToCreate.Id = _slider.Insert(entityToCreate);
                    entityToCreate.flag = (int)flag.Add;
                }
                else
                {
                    _slider.Update(entityToCreate);
                    entityToCreate.flag = (int)flag.Update;
                }

                return Json(new { ok = true, flag = entityToCreate.flag, objrow = _slider.Get(entityToCreate.Id) }, JsonRequestBehavior.AllowGet);    
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [HttpPost]
        public JsonResult Delete(int Id)
        {
            try
            {
                var model = _slider.Get(Id);

                if(!string.IsNullOrEmpty(model.ImageUrl))
                {
                    DeleteImage(model.ImageUrl);
                }

                return Json(new { ok = _slider.Delete(Id) }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        #region private functions

        public JsonResult Upload(HttpPostedFileBase file)
        {
            try
            {
                var versions = GetVersions();
                              
                string uploadFolder = System.Web.HttpContext.Current.Server.MapPath("~/Content/Photo/Slider");

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

                return Json(new { ok = true, FileName = imageUrl, FilePath = "/Content/Photo/Slider/_1921x987/" + imageUrl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, FileName = string.Empty, errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        private void DeleteImage(string filename)
        {
            var versions = GetVersions();

            string uploadFolder = System.Web.HttpContext.Current.Server.MapPath("~/Content/Photo/Slider/");

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

            versions.Add("_1921x987", "width=1921&height=987&crop=auto&format=jpg");

            return versions;
        }

        #endregion
    }
}

