namespace Suftnet.Cos.BackOffice
{
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using System.Web.Mvc;

    public class DiscountController : TenantCommonController
    {            
         private readonly ITenantCommon iCommon;

         public DiscountController(ITenantCommon iCommon)
            : base(iCommon)
        {
            this.iCommon = iCommon;
        }           

        public ActionResult Index()
        {            
            return View(iCommon.GetAll((int)eSettings.Discount));
        }
    }
}