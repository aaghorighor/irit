namespace Suftnet.Cos.Web
{
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Web.ViewModel;
    using System.Collections.Generic;
    using System.Web.Mvc; 
    public class SectionController : MainController
    {
        private readonly IChapter _support;
        private readonly ISettings _settings;

        public SectionController(IChapter support, ISettings settings)
        {
            _support = support;
            _settings = settings;
        }

        [OutputCache(Duration = 10, VaryByParam = "*")]
        public ActionResult Entry(int Id)
        {
            var model = new SectionModel
            {
                Header = Header(Id),
                Sections = Sections(Id)
            };

            return View(model);
        }

        #region private function
        private SettingsDTO Header(int Id)
        {
            var model = _settings.Get(Id);
            return model;
        }
        private IEnumerable<ChapterDto> Sections(int Id)
        {
            var model = _support.GetAll(Id);
            return model;
        }

        #endregion
    }
}
