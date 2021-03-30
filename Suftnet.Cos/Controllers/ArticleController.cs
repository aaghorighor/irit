namespace Suftnet.Cos.Web
{
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Web.ViewModel;
    using System.Collections.Generic;
    using System.Threading.Tasks;
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
        public ActionResult Entry(int sectionId, int chapterId)
        {
            var model = new TopicModel
            {
                Section = Chapter(chapterId)         
            };

            return View(model);
        }

        [ChildActionOnly]
        public async Task<ActionResult> Sections(int Id)
        {
            var model = await Task.Run(() => _topic.Fetch(Id));
            return PartialView("_timeLineContainer", model);
        }

        #region private function
        private ChapterDto Chapter(int Id)
        {
            var model = _chapter.Get(Id);
            return model;
        }

        #endregion
    }
}
