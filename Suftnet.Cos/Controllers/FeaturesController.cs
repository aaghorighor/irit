namespace Suftnet.Cos.Web
{
    using System.Web.Mvc;
    using Suftnet.Cos.DataAccess;
    using System.Threading.Tasks;
    using Common;

    public class FeaturesController : MainController
    {
        private readonly ICommon iCommon;
        public FeaturesController(ICommon iCommon)
        {
            this.iCommon = iCommon;
        }

        [OutputCache(Duration = 10, VaryByParam = "*")]
        public async Task<ActionResult> Index()
        {
            var model = await System.Threading.Tasks.Task.Run(() => iCommon.GetAll((int)eSettings.Feature));
            return View(model);
        }               
    }
}
