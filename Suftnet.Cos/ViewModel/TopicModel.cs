namespace Suftnet.Cos.Web.ViewModel
{
    using Suftnet.Cos.DataAccess;  
    using System.Collections.Generic;
  
    public class TopicModel
    {
        public ChapterDto Section { get; set; }
        public IEnumerable<TopicDto> Topics { get; set; }
    }
}