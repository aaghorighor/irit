namespace Suftnet.Cos.SiteAdmin.Controllers
{
    using Common;
    using Suftnet.Cos.CommonController.Controllers;
    using Suftnet.Cos.DataAccess;

    [AdminAuthorizeActionFilter(Constant.SiteAdminOnly)]
    public class SliderController : Suftnet.Cos.Admin.Controllers.SliderController
    {     
        public SliderController(ISlider slider):base(slider)
        {

        }

    }
}

