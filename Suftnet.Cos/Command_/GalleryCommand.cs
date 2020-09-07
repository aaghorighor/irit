namespace Suftnet.Cos.Web.Command
{
    using System;
    using Suftnet.Cos.DataAccess;
    using System.Web;
    using Suftnet.Cos.Extension;  
    using System.Linq;
    using System.IO; 
    using System.Net;
    using System.Collections.Generic;
    using ImageResizer;
    using Core;

    public class GalleryCommand : ICommand
    {
        private IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
        public IList<string> Reason = new List<string>();
        private int MaxContentLength = 1024 * 1024 * 1;      
        public HttpStatusCode HttpStatusCode { get; set; }
        private readonly IGallery _iGallery;
        public GalleryCommand(IGallery iGallery)
        {
            _iGallery = iGallery;          
        }       
        public HttpRequest HttpContext { get; set; }
        public string UPloadFolder { get; set; }
             
        public void Execute()
        {
          this.PrepareFileUpload();             
        }

        #region private function        

        private void PrepareFileUpload()
        {
           var albumId = PrepareAlbumId();
           HttpFileCollection httpFileCollections = HttpContext.Files;
           var count = httpFileCollections.Count;

           for(var index = 0; index < httpFileCollections.Count; index++)
            {
                var ext = httpFileCollections[index].FileName.Substring(httpFileCollections[index].FileName.LastIndexOf('.'));
                var extension = ext.ToLower();
                var fileName = httpFileCollections[index].FileName;

                if (!AllowedFileExtensions.Contains(extension))
                {
                    var message = string.Format("Please Upload image of type .jpg,.gif,.png. " + fileName);
                    Reason.Add(message);
                }                
                else
                {
                    try
                    {
                        Upload(httpFileCollections[index], albumId);                      
                      
                    }
                    catch (Exception ex)
                    {
                        Reason.Add("Error occurred while uploading image. " + fileName);
                        var logger = GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>();
                        logger.LogError(ex);
                    }

                }            
            }          
            
        }
        private int PrepareAlbumId()
        {
            if (HttpContext.Form.AllKeys.Any())
            {
                var albumId = HttpContext.Form["albumId"];
                return albumId.ToInt();
            }

            return 0;
        }
        public void Upload(HttpPostedFile file, int albumId)
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
                    var imageUrl = path.Substring(index + 1);

                    if(!string.IsNullOrEmpty(imageUrl))
                    {
                        PrepareGallery(albumId, path.Substring(index + 1));
                    }                                   
                }
            }
           
        }
        private void PrepareGallery(int albumId, string imageUrl)
        {
            var model = new GalleryDto
            {
                ImageUrl = imageUrl.ToImage(),
                Publish = true,
                CreatedBy = Environment.UserName,
                CreatedDT = DateTime.UtcNow,
                AlbumId = albumId
            };

            _iGallery.Insert(model);
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