namespace Suftnet.Cos.Web.Command
{
    using System;
    using Suftnet.Cos.DataAccess;
      
    using System.IO;
    using System.Collections.Generic;
    using Core;

    public class DeleteGalleryCommand : ICommand
    {       
        private readonly IGallery _gallery;
        public DeleteGalleryCommand(IGallery gallery)
        {
            _gallery = gallery;                   
        }

        public DeleteGalleryModel DeleteGalleryModel { get; set; }
        public string ImagePath { get; set; }

        public void Execute()
        {
            this.DeleteGallery();
        }

        #region private function
        private void DeleteGallery()
        {
            try
            {
                var model = _gallery.Get(DeleteGalleryModel.GalleryId);

                if (!string.IsNullOrEmpty(model.ImageUrl))
                {
                    DeleteImage(model.ImageUrl);
                }

                _gallery.Delete(DeleteGalleryModel.GalleryId);
            }

            catch (Exception ex)
            {
                GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>().LogError(ex);
            }
        }
        private void DeleteImage(string filename)
        {
            var versions = GetVersions();
      
            foreach (string suffix in versions.Keys)
            {
                string filePath = Path.Combine(ImagePath + "\\" + suffix, filename);

                if (!System.IO.File.Exists(filePath))
                {
                    continue;
                }

                new List<string>(Directory.GetFiles(ImagePath + suffix)).ForEach(files =>
                {
                    if (files.IndexOf(filename, StringComparison.OrdinalIgnoreCase) >= 0)
                        System.IO.File.Delete(files);
                });
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