namespace Suftnet.Cos.Web.Command
{
    using System;
    using Suftnet.Cos.DataAccess;

    using System.IO;
    using System.Collections.Generic;
    using Core;
    using System.Threading.Tasks;

    public class DeleteAlbumCommand : ICommand
    {       
        private readonly IAlbum _album;
        private readonly IGallery _gallery;
        private readonly IFactoryCommand _factoryCommand;
        public DeleteAlbumCommand(IAlbum album, IGallery gallery, IFactoryCommand factoryCommand)
        {
            _album = album;
            _gallery = gallery;
            _factoryCommand = factoryCommand;
        }

        public DeleteAlbumModel DeleteAlbumModel { get; set; }  
        public string ImagePath { get; set; }
       
        public void Execute()
        {
            this.DeleteAlbum();
        }

        #region private function
        private void DeleteAlbum()
        {
            try
            {
                var model = _album.Get(DeleteAlbumModel.AlbumId);

                if (!string.IsNullOrEmpty(model.ImageUrl))
                {
                    DeleteImage(model.ImageUrl);
                }

                if(DeleteGallery(DeleteAlbumModel.AlbumId))
                {
                    _album.Delete(DeleteAlbumModel.AlbumId);
                }               
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

        private bool DeleteGallery(int albumId)
        {
            var command = _factoryCommand.Create<DeleteGalleryCommand>();
            var galleries = _gallery.Fetch(albumId);

            foreach(var gallery in galleries)
            {               
                command.DeleteGalleryModel = new DeleteGalleryModel { GalleryId = gallery.Id };
                command.Execute();
            }

            return true;
        }
        #endregion

    }
}