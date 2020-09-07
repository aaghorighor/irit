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
    using Suftnet.Cos.Extension;
   
    public class TopicController : AdminBaseController
    {
        #region Resolving dependencies
             
        private readonly ITopic _topic;

        public TopicController(ITopic topic)
        {
            _topic = topic;

        }
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Entry(string topic, int Id)
        {
            return View(_topic.GetAll(Id));
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            try
            {
                return Json(new { ok = true, dataobject = _topic.Get(id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ValidateInput(false)]
        public JsonResult Create(TopicDto entityToCreate)
        {
            try
            {                 
                    entityToCreate.CreatedBy = this.UserName;
                    entityToCreate.CreatedDT = DateTime.Now;

                if (entityToCreate.Id == 0)
                {
                    if(!_topic.Find(entityToCreate.TopicId, entityToCreate.ChapterId))
                    {
                        entityToCreate.Id = _topic.Insert(entityToCreate);
                        entityToCreate.flag = (int)flag.Add;
                    }else
                    {
                        return Json(new { ok = false, msg = Constant.DuplicateTopicNotAllowed }, JsonRequestBehavior.AllowGet);
                    }
                 
                }
                else
                {
                    _topic.Update(entityToCreate);
                    entityToCreate.flag = (int)flag.Update;
                }

                var model = _topic.Get(entityToCreate.Id);
                model.Topic.ToFriendlyUrl();

                return Json(new { ok = true, flag = entityToCreate.flag, objrow = model }, JsonRequestBehavior.AllowGet);

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
                var test = _topic.Get(Id);

                if (!string.IsNullOrEmpty(test.ImageUrl))
                {
                    DeleteImage(test.ImageUrl);
                }

                return Json(new { ok = _topic.Delete(Id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        #region private functions

        public virtual JsonResult Upload(HttpPostedFileBase file)
        {
            try
            {
                var versions = GetVersions();

                string uploadFolder = System.Web.HttpContext.Current.Server.MapPath("~/content/photo/support");

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

                return Json(new { ok = true, FileName = imageUrl, FilePath = "/content/photo/support/_630x455/" + imageUrl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, FileName = string.Empty, errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        private void DeleteImage(string filename)
        {
            var versions = GetVersions();

            string uploadFolder = System.Web.HttpContext.Current.Server.MapPath("~/content/photo/support/");

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
            versions.Add("_630x455", "width=1184&height=561&crop=auto&format=jpg");
            return versions;
        }

        #endregion
    }
}
