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
        private readonly IOrderDetail _orderDetail;

        public CartController(ICategory category, IMenu menu, IOrderDetail orderDetail)
        {
            _orderDetail = orderDetail;
            _menu = menu;
            _category = category;
        }
        #endregion
        [OutputCache(Duration = 10, VaryByParam = "*")]
        public ActionResult Entry(string orderId)
        {           
            return View();         
        }
       

    }
}

