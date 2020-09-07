namespace Suftnet.Cos.Admin.Controllers
{
    using Common;
    using Suftnet.Cos.CommonController.Controllers;

    [AdminAuthorizeActionFilter(Constant.AdminOnly)]
    public class AdminBaseController : BaseController
    {                   
        
    }
}
