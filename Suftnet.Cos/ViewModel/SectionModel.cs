namespace Suftnet.Cos.Web.ViewModel
{
    using Suftnet.Cos.DataAccess;  
    using System.Collections.Generic;
  
    public class SectionModel
    {
        public SettingsDTO Header { get; set; }
        public IEnumerable<ChapterDto> Sections { get; set; }
    }
}