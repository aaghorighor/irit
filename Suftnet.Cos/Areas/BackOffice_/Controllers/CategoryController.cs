namespace Suftnet.Cos.BackOffice
{
    using System;
    using System.Web.Mvc;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Common;
    using System.Web;
    using System.IO;
    using System.Collections.Generic;
    using ImageResizer;
    using Web.ActionFilter;
    using System.Threading.Tasks;
    using Service;

    public class CategoryController : BackOfficeBaseController
    {
        #region Resolving dependencies

        private readonly ICategory _category;

        public CategoryController(ICategory category)
        {
            _category = category;            
        }

        #endregion

        #region 

        public async Task<ActionResult> Index()
        {
            var model = await System.Threading.Tasks.Task.Run(() => _category.GetAll());
            return View(model);
        }  


        [HttpGet]
        public async Task<JsonResult> Get(int Id)
        {
            try
            {
                Ensure.Argument.NotNull(Id);

                var model = await System.Threading.Tasks.Task.Run(() => _category.Get(Id));
                return Json(new { ok = true, dataobject = model }, JsonRequestBehavior.AllowGet);              
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }       
        
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]        
        public JsonResult Create(CategoryDto entityToCreate)
        {
            try
            {
                Ensure.Argument.NotNull(entityToCreate);

                entityToCreate.CreatedDT = DateTime.UtcNow;
                entityToCreate.CreatedBy = this.UserName;
            
                if (string.IsNullOrEmpty(entityToCreate.ImageUrl))
                {
                    entityToCreate.ImageUrl = "Blank.jpg";
                }

                if (entityToCreate.Id == 0)
                {                                  
                    entityToCreate.Id = _category.Insert(entityToCreate);
                    entityToCreate.flag = (int)flag.Add;                     
                }
                else
                {
                    _category.Update(entityToCreate);
                    entityToCreate.flag = (int)flag.Update;
                   
                }

                return Json(new { ok = true, flag = entityToCreate.flag, objrow = _category.Get(entityToCreate.Id) }, JsonRequestBehavior.AllowGet);
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
                var model = await System.Threading.Tasks.Task.Run(() => _category.Delete(Id));
                return Json(new { ok = true, dataobject = model }, JsonRequestBehavior.AllowGet);               
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        #endregion

        #region private function

        public JsonResult Upload(HttpPostedFileBase file)
        {
            try
            {
                var versions = GetVersions();

                //Get the physical path for the uploads folder and make sure it exists
                string uploadFolder = System.Web.HttpContext.Current.Server.MapPath("~/Content/Photo/Category");

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

                return Json(new { ok = true, FileName = imageUrl, FilePath = "/Content/Photo/Category/32x32/" + imageUrl }, JsonRequestBehavior.AllowGet);
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
            versions.Add("32x32", "width=32&height=32&crop=auto&format=jpg");
            versions.Add("180x120", "width=180&height=120&crop=auto&format=jpg");

            return versions;
        }

        #endregion

    }
}
