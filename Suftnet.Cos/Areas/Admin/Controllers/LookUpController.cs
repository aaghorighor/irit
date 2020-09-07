namespace Suftnet.Cos.Admin.Controllers
{
    using System;
    using System.Web.Mvc;
     using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using System.Collections.Generic;
    using System.IO;
    using ImageResizer;
    using System.Web;
    using Suftnet.Cos.Extension;

    public class LookUpController : AdminBaseController
    {       
        #region Resolving dependencies
   
        private readonly ISettings  _settings;

        public LookUpController(ISettings settings)
        {               
          
            _settings = settings;                  
        }
        #endregion
        
        #region settings
        public ActionResult Index()
        {      
            return View(_settings.GetAll());
        }             
               
        [HttpGet]
        public JsonResult Get(int Id)
        {
            try
            {                
                  return Json(new { ok = true, dataobject = _settings.Get(Id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        public ActionResult GetAll(int Id)
        {
            try
            {
                return Json( new { ok = true,  objrow = _settings.GetAllByID(Id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }
        
        [HttpPost]
        public JsonResult Create(SettingsDTO settingsToCreate)
        {          
            try
            {
                if (settingsToCreate.Id == 0)
                {
                    settingsToCreate.Id = _settings.Insert(new DataAccess.Action.Setting { ImageUrl = settingsToCreate.ImageUrl, ClassId = settingsToCreate.ClassId, Active = settingsToCreate.Active, CreatedBy = Environment.UserName, Description = settingsToCreate.Description, Title = settingsToCreate.Title, CreatedDT = DateTime.Now });
                    settingsToCreate.flag = (int)flag.Add;
                }
                else
                {
                    _settings.Update(new DataAccess.Action.Setting { ImageUrl = settingsToCreate.ImageUrl, ClassId = settingsToCreate.ClassId, ID = settingsToCreate.Id, Active = settingsToCreate.Active, CreatedBy = Environment.UserName, Description = settingsToCreate.Description, Title = settingsToCreate.Title, CreatedDT = DateTime.Now });
                     settingsToCreate.flag = (int)flag.Update;
                }

                var model = _settings.Get(settingsToCreate.Id);
                model.QueryTitle.ToFriendlyUrl();

                return Json(new { ok = true, Id =settingsToCreate.Id, flag = settingsToCreate.flag, objrow = model }, JsonRequestBehavior.AllowGet);
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
                bool response = _settings.Delete(Id);
                return Json(new { ok = response }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }

        }

        #endregion

        public JsonResult Upload(HttpPostedFileBase file)
        {
            try
            {
                var versions = GetVersions();

                //Get the physical path for the uploads folder and make sure it exists
                string uploadFolder = System.Web.HttpContext.Current.Server.MapPath("~/content/photo/settings");

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

                return Json(new { ok = true, FileName = imageUrl, FilePath = "/content/photo/settings/_630x455/" + imageUrl }, JsonRequestBehavior.AllowGet);
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

            string uploadFolder = System.Web.HttpContext.Current.Server.MapPath("~/content/photo/settings/");

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
          
            versions.Add("_630x455", "width=1918&height=967&crop=auto&format=jpg");
            return versions;
        }
        #endregion

    }
}
