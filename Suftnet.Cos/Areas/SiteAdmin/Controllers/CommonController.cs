namespace Suftnet.Cos.SiteAdmin.Controllers
{   
    using Suftnet.Cos.Common;
    using Suftnet.Cos.CommonController.Controllers;
    using Suftnet.Cos.DataAccess;
      

    [AdminAuthorizeActionFilter(Constant.SiteAdminOnly)]
    public class CommonController : Suftnet.Cos.Admin.Controllers.CommonController
    {
        public CommonController(ICommon common) : base(common)
        {

        }
    }
}
