namespace Suftnet.Cos.SiteAdmin.Controllers
{
    using Common;
    using Suftnet.Cos.CommonController.Controllers;
    using Suftnet.Cos.DataAccess;

    [AdminAuthorizeActionFilter(Constant.SiteAdminOnly)]
    public class FaqController : Suftnet.Cos.Admin.Controllers.FaqController
    {      
        public FaqController(IFaq faq) :base(faq)
        {

        }
    }
}

