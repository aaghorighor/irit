namespace Suftnet.Cos.Web.Command
{
    using System;
    using Suftnet.Cos.DataAccess;
    using System.Web;
    using Suftnet.Cos.Extension;
    using Microsoft.Web.Helpers;
    using System.Linq;
    using System.IO;
    using System.Net;
    using System.Collections.Generic;
    using ImageResizer;
    using Core;

    public class DevotionCommand : ICommand
    {
        private IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
        public IList<string> Reason = new List<string>();
        private int MaxContentLength = 1024 * 1024 * 1;
        private string ImageUrl { get; set; }
        public int Flag;
        public HttpStatusCode HttpStatusCode { get; set; }
        private readonly IDevotion _devotion;
        public DevotionCommand(IDevotion devotion)
        {
            _devotion = devotion;
        }

        public HttpRequest HttpContext { get; set; }
        public string UPloadFolder { get; set; }

        public void Execute()
        {
            this.PrepareFileUpload();
            if (this.HttpStatusCode == HttpStatusCode.Created)
            {

                switch (Flag)
                {
                    case 1:
                        Create();
                        break;
                    case 2:
                        Edit();
                        break;
                }
            }
        }

        #region private function

        private void Create()
        {
            if (HttpContext.Form.AllKeys.Any())
            {
                var title = HttpContext.Form["title"];
                var description = HttpContext.Form["description"];
                var externalId = HttpContext.Form["externalId"];
                var Active = HttpContext.Form["Active"];
                var Author = HttpContext.Form["Author"];
                var ShortDescription = HttpContext.Form["shortDescription"];
                var CreatedDT = HttpContext.Form["createdDT"];

                var model = new ArticleDto
                {
                    Title = title,
                    ImageUrl = ImageUrl.ToImage(),
                    Active = Active.ToBoolean(),
                    Description = description,
                    ShortDescription = ShortDescription,
                    Author = Author,

                    CreatedBy = Environment.UserName,
                    CreatedDT = CreatedDT.ToDate(),
                    TenantId = externalId.ToDecrypt().ToInt()
                };

                _devotion.Insert(model);
            }
        }
        private void Edit()
        {
            if (HttpContext.Form.AllKeys.Any())
            {

                var title = HttpContext.Form["title"];
                var description = HttpContext.Form["description"];
                var Active = HttpContext.Form["Active"];
                var Author = HttpContext.Form["Author"];
                var Id = HttpContext.Form["Id"];
                var ShortDescription = HttpContext.Form["shortDescription"];
                var CreatedDT = HttpContext.Form["createdDT"];

                var model = new ArticleDto
                {
                    Title = title,
                    ImageUrl = ImageUrl.ToImage(),
                    Active = Active.ToBoolean(),
                    Description = description,
                    ShortDescription = ShortDescription,
                    Author = Author,
                    Id = Id.ToInt(),
                    CreatedDT = CreatedDT.ToDate(),
                };

                _devotion.Update(model);
            }
        }
        private void PrepareFileUpload()
        {
            if (HttpContext.Files.AllKeys.Any())
            {
                var httpPostedFile = HttpContext.Files["File"];
                if (httpPostedFile != null)
                {
                    FileUpload fileUpload = new FileUpload();
                    var ext = httpPostedFile.FileName.Substring(httpPostedFile.FileName.LastIndexOf('.'));
                    var extension = ext.ToLower();

                    if (!AllowedFileExtensions.Contains(extension))
                    {
                        var message = string.Format("Please Upload image of type .jpg,.gif,.png.");
                        Reason.Add(message);
                        HttpStatusCode = HttpStatusCode.BadRequest;
                        return;
                    }
                    else
                    {
                        try
                        {
                            Upload(httpPostedFile);
                            Reason.Add("Image Uploaded Successfully.");
                            HttpStatusCode = HttpStatusCode.Created;
                        }
                        catch (Exception ex)
                        {
                            Reason.Add("Error occurred while uploading image");
                            HttpStatusCode = HttpStatusCode.NotFound;

                            var logger = GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>();
                            logger.LogError(ex);
                        }

                    }
                }
                else
                {
                    Reason.Add("Please Upload a image.");
                    HttpStatusCode = HttpStatusCode.NotFound;
                }
            }
        }
        public void Upload(HttpPostedFile file)
        {
            var guid = System.Guid.NewGuid().ToString();
            var path = string.Empty;

            var versions = GetVersions();

            if (!Directory.Exists(UPloadFolder)) Directory.CreateDirectory(UPloadFolder);

            foreach (string suffix in versions.Keys)
            {
                if (!Directory.Exists(UPloadFolder + "/" + suffix)) Directory.CreateDirectory(UPloadFolder + "/" + suffix);

                string fileName = Path.Combine(UPloadFolder + "\\" + suffix, guid);
                path = ImageBuilder.Current.Build(file, fileName, new ResizeSettings(versions[suffix]), false, true);

                int index = path.LastIndexOf('\\');
                if (index != -1)
                {
                    ImageUrl = path.Substring(index + 1);
                }
            }
        }
        private Dictionary<string, string> GetVersions()
        {
            Dictionary<string, string> versions = new Dictionary<string, string>();
            versions.Add("500x500", "width=500&height=500&crop=auto&format=jpg");
            return versions;
        }

        #endregion
    }
}