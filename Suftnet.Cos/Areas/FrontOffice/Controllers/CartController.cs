namespace Suftnet.Cos.FrontOffice
{
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Service;
    using Suftnet.Cos.Web.Command;
    using Suftnet.Cos.Web.Infrastructure.ActionFilter;
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    public class CartController : FrontOfficeBaseController
    {
        #region Resolving dependencies

        private readonly ICategory _category;
        private readonly IMenu _menu;    
        private readonly IOrder _order;    
        private readonly IOrderCommand _orderCommand;

        public CartController(ICategory category, IMenu menu, IOrderCommand orderCommand,
          IOrder order)
        {           
            _menu = menu;
            _category = category;
            _order = order;      
            _orderCommand = orderCommand;
        }
        #endregion
        [OutputCache(Duration = 0, VaryByParam = "*")]
        public ActionResult Entry(string orderId, string orderTypeId, string orderType, string orderStatusId, string deliveryCost)
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> FetchCart(Guid orderId)
        {
            return Json(new { ok = true, dataobject = await Task.Run(() => _order.FetchOrder(orderId)) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> FetchCategories()
        {
            return Json(new { ok = true, dataobject = await Task.Run(() => _category.GetBy(true, this.TenantId)) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> FetchDefaultMenues()
        {
            return Json(new { ok = true, dataobject = await Task.Run(() => _menu.GetBy(this.TenantId, 20)) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> FetchMenuByCategory(Guid categoryId)
        {           
            return Json(new { ok = true, dataobject = await Task.Run(() => _menu.GetByCategoryId(categoryId, this.TenantId)) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]       
        [ValidateHeaderAntiForgeryToken]
        public JsonResult Create(OrderedSummaryDto entityToCreate)
        {
            Ensure.Argument.NotNull(entityToCreate);

             entityToCreate.AccountTypeId = new Guid(eAccountType.Delivery);

            _orderCommand.TenantId = this.TenantId;
            _orderCommand.OrderedSummary = entityToCreate;
            _orderCommand.CreatedBy = this.UserName;
            _orderCommand.CreatedDt = DateTime.UtcNow;
            _orderCommand.Execute();                        

            return Json(new { ok = true});
        }       

    }
}

