namespace Suftnet.Cos.BackOffice
{
    using Suftnet.Cos.DataAccess;
    using System.Web.Mvc;
    using Suftnet.Cos.Extension;   
    using System.Threading.Tasks;
    using Suftnet.Cos.Service;

    public class LookupController : TenantCommonController
    {
        private readonly ITenantCommon _common;

        public LookupController(ITenantCommon iCommon)
            : base(iCommon)
        {
            _common = iCommon;
        }             

        public ActionResult Entry(string settingId, int sectionId)
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> Fetch(string Id)
        {
            Ensure.Argument.NotNull(Id);

            var model = await Task.Run(() => _common.GetAll(Id.ToDecrypt().ToInt(), this.TenantId));
            return Json(new { data = model }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Switch(int sectionId)
        {
            var url = string.Empty;
            UrlHelper urlHelper = new UrlHelper(Request.RequestContext); 

            switch (sectionId)
            {               
                case 1:
                    url = urlHelper.Action("Index", "Giving");
                    break;                          

                case 2:
                    url = urlHelper.Action("Index", "Asset");
                    break;               

                case 3:
                    url = urlHelper.Action("Index", "Event");
                    break;                                                 

                default :
                    break;
            }

          return  Redirect(url);
        }
    }
}