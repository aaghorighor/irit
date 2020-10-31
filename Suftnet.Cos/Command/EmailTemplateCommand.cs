namespace Suftnet.Cos.Web.Command
{
    using System;
    using Core;  
    using System.IO;

    public class EmailTemplateCommand : ICommand
    {      
        public string View;    
        public string VIEW_PATH;

        public EmailTemplateCommand()
        {            
        }                 
       
        public void Execute()
        {
            this.CreateView();
        }

        #region private function

        private void CreateView()
        {
            this.View = ReadFile();
        }

        private string ReadFile()
        {
            try
            {
                string htmlString = File.ReadAllText(this.VIEW_PATH);
                return htmlString;
            }catch(Exception ex)
            {
                GeneralConfiguration.Configuration.Logger.LogError(ex);
            }

            return string.Empty;          
        }
       
        #endregion

    }
}