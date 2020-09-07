namespace Suftnet.Cos.Web
{
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Web.ViewModel;
    using System.Collections.Generic;
    using System.Web.Mvc; 
    public class ArticleController : MainController
    {
        private readonly IChapter _chapter;
        private readonly ITopic _topic;

        public ArticleController(IChapter chapter, ITopic topic)
        {
            _chapter = chapter;
            _topic = topic;
        }
        public ActionResult Entry(int sectionId, int supportId)
        {
            var model = new TopicModel
            {
                Section = Section(supportId),
                Topics = Topics(supportId)
            };

            return View(model);
        }

        #region private function
        private ChapterDto Section(int Id)
        {
            var model = _chapter.Get(Id);
            return model;
        }
        private IEnumerable<TopicDto> Topics(int Id)
        {
            var model = _topic.Fetch(Id);
            return model;
        }

        #endregion
    }
}
