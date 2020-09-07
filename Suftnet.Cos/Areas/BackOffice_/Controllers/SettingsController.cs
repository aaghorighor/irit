namespace Suftnet.Cos.BackOffice
{
    using CommonController.Controllers;  
    using ImageResizer;
    using Service;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using Web.Mapper; 
    using Web.ViewModel;
    using Suftnet.Cos.Extension;
    using Suftnet.Cos.Web.Command;
       
    public class SettingsController : BackOfficeBaseController
    {
        #region Resolving dependencies        
  
        private readonly ITenant _tenant;       
        private readonly IAddress _address;
        private readonly IFactoryCommand _factoryCommand;

        public SettingsController(IAddress address, ITenant tenant,
               IFactoryCommand factoryCommand)
        {
            _tenant = tenant;               
            _address = address;
            _factoryCommand = factoryCommand;

        }
        #endregion

        [OutputCache(Duration = 10, VaryByParam = "*")]
        public ActionResult Index()
        {            
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> OverView()
        {
            var model = await Task.Run(() => _tenant.Get(this.TenantId));
            return Json(new { ok= true, dataobject = model },JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [PermissionFilter(BackOfficeViews.Settings, PermissionType.Create)]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(TenantModel entityToCreate)
        {
            Ensure.Argument.NotNull(entityToCreate.Id);
            Ensure.Argument.IsNot(entityToCreate.Id == 0);
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

            entityToCreate.CreatedBy = this.UserName;
            entityToCreate.CreatedDT = DateTime.UtcNow;
            entityToCreate.TenantId = this.TenantId;

            _address.UpdateByAddressId(entityToCreate);           
            _tenant.Update(Map.From(entityToCreate));

            Task.Run(()=> CreatePushNotification(entityToCreate));           
           
            return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public JsonResult UpdateStartUp()
        {
            _tenant.UpdateStartUp(this.TenantId, true);          
            return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
        }

        #region private
        public JsonResult UploadBackground(HttpPostedFileBase file)
        {
            try
            {
                var versions = BackgroundVersions();
                               
                string uploadFolder = System.Web.HttpContext.Current.Server.MapPath("~/content/photo/background");

                if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);
                              
                var imageUrl = string.Empty;
                var guid = System.Guid.NewGuid().ToString();
                var path = string.Empty;

                foreach (string suffix in versions.Keys)
                {
                    if (!Directory.Exists(uploadFolder + "/" + suffix)) Directory.CreateDirectory(uploadFolder + "/" + suffix);                                       
                    string fileName = Path.Combine(uploadFolder + "\\" + suffix, guid);                                      
                    path = ImageBuilder.Current.Build(file, fileName, new ResizeSettings(versions[suffix]), false, true);

                    int lastIndex = path.LastIndexOf('\\');
                    if (lastIndex != -1)
                    {
                        imageUrl = path.Substring(lastIndex + 1);
                    }
                }

                return Json(new { ok = true, FileName = imageUrl, FilePath = "/content/photo/background/500x500/" + imageUrl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, FileName = string.Empty, errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UploadPastor(HttpPostedFileBase file)
        {
            try
            {
                var versions = PastorVersions();

                string uploadFolder = System.Web.HttpContext.Current.Server.MapPath("~/content/photo/pastor");

                if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                var imageUrl = string.Empty;
                var guid = System.Guid.NewGuid().ToString();
                var path = string.Empty;

                foreach (string suffix in versions.Keys)
                {
                    if (!Directory.Exists(uploadFolder + "/" + suffix)) Directory.CreateDirectory(uploadFolder + "/" + suffix);
                    string fileName = Path.Combine(uploadFolder + "\\" + suffix, guid);
                    path = ImageBuilder.Current.Build(file, fileName, new ResizeSettings(versions[suffix]), false, true);

                    int lastIndex = path.LastIndexOf('\\');
                    if (lastIndex != -1)
                    {
                        imageUrl = path.Substring(lastIndex + 1);
                    }
                }

                return Json(new { ok = true, FileName = imageUrl, FilePath = "/content/photo/pastor/500x500/" + imageUrl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, FileName = string.Empty, errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UploadLogo(HttpPostedFileBase file)
        {
            try
            {
                var versions = LogoVersions();
                              
                string uploadFolder = System.Web.HttpContext.Current.Server.MapPath("~/content/photo/logo");

                if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

                var imageUrl = string.Empty;
                var guid = System.Guid.NewGuid().ToString();
                var path = string.Empty;

                foreach (string suffix in versions.Keys)
                {
                    if (!Directory.Exists(uploadFolder + "/" + suffix)) Directory.CreateDirectory(uploadFolder + "/" + suffix);
                    string fileName = Path.Combine(uploadFolder + "\\" + suffix, guid);
                    path = ImageBuilder.Current.Build(file, fileName, new ResizeSettings(versions[suffix]), false, true);

                    int lastIndex = path.LastIndexOf('\\');
                    if (lastIndex != -1)
                    {
                        imageUrl = path.Substring(lastIndex + 1);
                    }
                }

                return Json(new { ok = true, FileName = imageUrl, FilePath = "/content/photo/logo/100x100/" + imageUrl }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { ok = false, FileName = string.Empty, errors = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        private Dictionary<string, string> LogoVersions()
        {
            Dictionary<string, string> versions = new Dictionary<string, string>();

            versions.Add("100x100", "width=100&height=100&crop=auto&format=jpg");

            return versions;
        }
        private Dictionary<string, string> PastorVersions()
        {
            Dictionary<string, string> versions = new Dictionary<string, string>();

            versions.Add("500x500", "width=500&height=500&crop=auto&format=jpg");

            return versions;
        }
        private Dictionary<string, string> BackgroundVersions()
        {
            Dictionary<string, string> versions = new Dictionary<string, string>();

            versions.Add("500x500", "width=500&height=500&crop=auto&format=jpg");

            return versions;
        } 
        private async void CreatePushNotification(TenantModel entityToCreate)
        {
            if (entityToCreate.PushNotification == true)
            {
                try
                {
                    var command = _factoryCommand.Create<PushNotificationCommand>();

                    command.Title = entityToCreate.Name + "app updates";
                    command.Body = entityToCreate.Name + "app updates";
                    command.TenantId = this.TenantId;
                    command.Id = entityToCreate.SliderHomeOptionId.ToString();
                    command.ClickAction = "settingActivity";

                    await Task.Run(() => command.Execute());
                }
                catch (Exception ex)
                {
                    Logger(ex);
                }
            }           
        }

        #endregion
    }
}

