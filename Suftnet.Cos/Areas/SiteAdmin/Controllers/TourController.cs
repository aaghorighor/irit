namespace Suftnet.Cos.SiteAdmin.Controllers
{
    using Common;
    using Suftnet.Cos.CommonController.Controllers;
    using Suftnet.Cos.DataAccess;

    [AdminAuthorizeActionFilter(Constant.SiteAdminOnly)]
    public class TourController : Suftnet.Cos.Admin.Controllers.TourController
    {
        #region Resolving dependencies       

        public TourController(ITour tour) :base(tour)
        {                 

        }
        #endregion

      
    }
}
