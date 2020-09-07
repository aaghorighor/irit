namespace Suftnet.Cos.Web
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Suftnet.Cos.DataAccess;

    public class TourController : MainController
    {
        private readonly ITour _tour;
        public TourController(ITour tour)
        {
            _tour = tour;
        }

        [OutputCache(Duration = 10, VaryByParam = "*")]
        public async Task<ActionResult> Index()
        {
            var model = await Task.Run(() => _tour.LoadTours());
            return View(model);        
        }               
    }
}
