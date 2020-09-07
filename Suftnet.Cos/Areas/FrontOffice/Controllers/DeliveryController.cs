namespace Suftnet.Cos.FrontOffice
{ 
    using System.Web.Mvc;
    using Suftnet.Cos.DataAccess; 
    using System.Linq;
    using Suftnet.Cos.Web;
    using Suftnet.Cos.Common;
    using System;

    public class DeliveryController : OrderController
    {     
        #region Resolving dependencies

        private readonly IOrder _order;
        private readonly ITable _table;

        public DeliveryController(IOrder order, ITable table) :base(order, table)
        {
            _order = order;
            _table = table;
        }

        #endregion       
        public override JsonResult Fetch(DataTableAjaxPostModel param)
        {
            var model = _order.GetDeliveryOrders(new Guid(eOrderType.Delivery), this.TenantId, param.start, param.length, param.search.value);

            return Json(new
            {
                draw = param.draw,
                recordsTotal = model.Count(),
                recordsFiltered = _order.Count(this.TenantId, new Guid(eOrderType.Delivery)),
                data = model
            },
                      JsonRequestBehavior.AllowGet);
        }       
    }
}

