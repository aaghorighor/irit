namespace Suftnet.Cos.FrontOffice
{
    using System;
    using System.Web.Mvc;
    using Suftnet.Cos.DataAccess;
     
    public class CartController : FrontOfficeBaseController
    {     
        #region Resolving dependencies
              
        private readonly ICategory _category;
        private readonly IMenu _menu;
        private readonly IOrderDetail _cart;
        private readonly IOrder _order;

        public CartController(ICategory category, IMenu menu, IOrderDetail cart, IOrder order)
        {
            _cart = cart;
            _menu = menu;
            _category = category;
            _order = order;
        }
        #endregion
        [OutputCache(Duration = 0, VaryByParam = "*")]
        public ActionResult Entry(string orderId, string orderTypeId, string orderType)
        {           
            return View();         
        }

        public JsonResult FetchOrderBy(Guid orderId)
        {
            return Json(new { ok = true, dataobject = _order.Get(orderId) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FetchCategories()
        {
            return Json(new { ok = true, dataobject = _category.GetByStatus(true, this.TenantId) }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FetchDefaultMenues()
        {
            return Json(new { ok = true, dataobject = _menu.GetByDefault(this.TenantId, 10) }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FetchMenuByCategory(Guid categoryId)
        {
            return Json(new { ok = true, dataobject = _menu.GetByCategoryId(categoryId, this.TenantId) }, JsonRequestBehavior.AllowGet);
        }


    }
}

