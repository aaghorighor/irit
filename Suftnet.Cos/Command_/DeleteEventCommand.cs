namespace Suftnet.Cos.Web.Command
{
    using System;
    using Suftnet.Cos.DataAccess;

    using System.IO;
    using System.Collections.Generic;
    using Core;
  
    public class DeleteEventCommand : ICommand
    {       
        private readonly IEvent _iEvent;       
        private readonly IFactoryCommand _factoryCommand;
        public DeleteEventCommand(IEvent iEvent, IFactoryCommand factoryCommand)
        {
            _iEvent = iEvent;           
            _factoryCommand = factoryCommand;
        }

        public EventIdModel EventIdModel { get; set; }  
        public string ImagePath { get; set; }
       
        public void Execute()
        {
            this.Delete();
        }

        #region private function
        private void Delete()
        {
            try
            {
                var model = _iEvent.Get(EventIdModel.EventId);

                if (!string.IsNullOrEmpty(model.ImageUrl))
                {
                    _iEvent.Delete(EventIdModel.EventId);
                     DeleteImage(model.ImageUrl);
                }                           
            }

            catch (Exception ex)
            {              
                GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>().LogError(ex);
            }
        }
        private void DeleteImage(string filename)
        {
            try
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
            catch (Exception ex)
            {
                GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>().LogError(ex);
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