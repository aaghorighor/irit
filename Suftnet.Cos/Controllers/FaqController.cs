namespace Suftnet.Cos.Web
{
    using Suftnet.Cos.DataAccess;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class FaqController : MainController
    {
        private readonly IFaq _faq;
           
        public FaqController(IFaq faq)
        {
            _faq = faq;
        }

        [OutputCache(Duration = 10, VaryByParam = "*")]
        public async Task<ActionResult> Index()
        {
            var model = await Task.Run(() => _faq.LoadFaq());
            return View(model);        
        }      
    }
}
