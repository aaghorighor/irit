namespace Suftnet.Cos.BackOffice
{
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using System.Web.Mvc;

    public class AddonTypeController : TenantCommonController
    {            
         private readonly ITenantCommon iCommon;

         public AddonTypeController(ITenantCommon iCommon)
            : base(iCommon)
        {
            this.iCommon = iCommon;
        }           

        public ActionResult entry(int menuId)
        {            
            return View(iCommon.GetAll((int)eSettings.AddonType));
        }
    }
}