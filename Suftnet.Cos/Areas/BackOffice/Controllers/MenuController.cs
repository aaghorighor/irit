namespace Suftnet.Cos.BackOffice
{
    using System;
    using System.Web.Mvc;

    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.CommonController.Controllers;
    using Suftnet.Cos.Extension;
    using Service;
    using System.Linq;
    using Suftnet.Cos.Web;
    using System.Web;
    using System.IO;
    using ImageResizer;
    using System.Collections.Generic;

    public class MenuController : BackOfficeBaseController
    {     
        #region Resolving dependencies

        private readonly IMenu _menu;     

        public MenuController(IMenu menu)
        {
            _menu = menu;             
        }
        #endregion
        [OutputCache(Duration = 10, VaryByParam = "*")]
        public ActionResult Index()
        {           
            return View();         
        }
        public JsonResult Fetch(DataTableAjaxPostModel param)
        {
            var model = _menu.GetAll(this.TenantId, param.start, param.length, param.search.value);

            return Json(new
            {
                draw = param.draw,
                recordsTotal = model.Count(),
                recordsFiltered = _menu.Count(this.TenantId),
                data = model
            },
                      JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[PermissionFilter(BackOfficeViews.Member, PermissionType.Create)]
        [ValidateAntiForgeryToken]
        public JsonResult Create(MenuDto entityToCreate)
        {
            Ensure.Argument.NotNull(entityToCreate);

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    ok = false,
                    isValid = true,
                    errors = ModelState.AjaxErrors()
                });
            }

            entityToCreate.CreatedDT = DateTime.UtcNow;
            entityToCreate.CreatedBy = this.UserName;

            entityToCreate.TenantId = this.TenantId;
            entityToCreate.Id = Guid.NewGuid();

            _menu.Insert(entityToCreate);
            entityToCreate.flag = (int)flag.Add;

            return Json(new { ok = true, flag = entityToCreate.flag }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[PermissionFilter(BackOfficeViews.Member, PermissionType.Edit)]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(MenuDto entityToCreate)
        {
            Ensure.Argument.NotNull(entityToCreate);

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    ok = false,
                    isValid = true,
                    errors = ModelState.AjaxErrors()
                });
            }
                     
           _menu.Update(entityToCreate);
            entityToCreate.flag = (int)flag.Update;

            return Json(new { ok = true, flag = entityToCreate.flag }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //[PermissionFilter(BackOfficeViews.Member, PermissionType.Remove)]
        public JsonResult Delete(string Id)
        {
            Ensure.NotNull(Id);
            return Json(new { ok = _menu.Delete( new Guid(Id)) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Upload(HttpPostedFileBase file)
        {
            try
            {
                var versions = GetVersions();

                string uploadFolder = System.Web.HttpContext.Current.Server.MapPath("~/content/photo/menu");

                if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                var imageUrl = string.Empty;
                var guid = System.Guid.NewGuid().ToString();
                var path = string.Empty;

                foreach (string suffix in versions.Keys)
                {
                    if (!Directory.Exists(uploadFolder + "/" + suffix)) Directory.CreateDirectory(uploadFolder + "/" + suffix);
                    string fileName = Path.Combine(uploadFolder + "\\" + suffix, guid);
                    path = ImageBuilder.Current.Build(file, fileName, new ResizeSettings(versions[suffix]), false, true);

                    int index = path.LastIndexOf('\\');
                    if (index != -1)
                    {
                        imageUrl = path.Substring(index + 1);
                    }
                }

                return Json(new { ok = true, FileName = imageUrl, FilePath = "/content/photo/menu/216x196/" + imageUrl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, FileName = string.Empty, errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        private Dictionary<string, string> GetVersions()
        {
            Dictionary<string, string> versions = new Dictionary<string, string>();
            versions.Add("216x196", "width=216&height=196&crop=auto&format=jpg");

            return versions;
        }

    }
}

