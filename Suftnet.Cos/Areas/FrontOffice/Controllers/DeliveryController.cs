namespace Suftnet.Cos.FrontOffice
{ 
    using System.Web.Mvc;
    using Suftnet.Cos.DataAccess; 
    using System.Linq;
    using Suftnet.Cos.Web;
    using Suftnet.Cos.Common;
    using System;
    using Suftnet.Cos.CommonController.Controllers;
    using Suftnet.Cos.Service;
    using Suftnet.Cos.Extension;
    using System.Threading.Tasks;

    public class DeliveryController : FrontOfficeBaseController
    {     
        #region Resolving dependencies

        private readonly IOrder _order;
        private readonly ICustomerOrder _customerOrder;

        public DeliveryController(IOrder order, ICustomerOrder customerOrder) 
        {
            _order = order;
            _customerOrder = customerOrder;
        }

        #endregion      
        [OutputCache(Duration = 10, VaryByParam = "*")]
        public virtual ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> Fetch(DataTableAjaxPostModel param)
        {
            var model = await Task.Run(() => _customerOrder.Fetch(this.TenantId, param.start, param.length, param.search.value));

            return Json(new
            {
                draw = param.draw,
                recordsTotal = model.Count(),
                recordsFiltered = _order.CountByOrderType(this.TenantId, new Guid(eOrderType.Delivery)),
                data = model
            },
                      JsonRequestBehavior.AllowGet);
        }     

        [HttpPost]
        [PermissionFilter(BackOfficeViews.Member, PermissionType.Remove)]
        public JsonResult Delete(string Id)
        {
            Ensure.NotNull(Id);
            return Json(new { ok = _order.Delete(new Guid(Id)) }, JsonRequestBehavior.AllowGet);
        }      
        
    }
}

