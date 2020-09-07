namespace Suftnet.Cos.SiteAdmin.Controllers
{
    using ImageResizer;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.CommonController.Controllers;
    using Suftnet.Cos.DataAccess;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;

    [AdminAuthorizeActionFilter(Constant.SiteAdminOnly)]
    public class SubTopicController : Suftnet.Cos.Admin.Controllers.SubTopicController
    {
        public SubTopicController(ISubTopic topic) : base(topic)
        {
        }

        public override JsonResult Upload(HttpPostedFileBase file)
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
    }
}