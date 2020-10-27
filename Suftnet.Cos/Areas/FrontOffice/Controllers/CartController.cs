namespace Suftnet.Cos.FrontOffice
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.Core;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Service;
    using Suftnet.Cos.Web.Command;
    using Suftnet.Cos.Web.Infrastructure.ActionFilter;

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
            return Json(new { ok = true, dataobject = await Task.Run(() => _category.GetByStatus(true, this.TenantId)) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> FetchDefaultMenues()
        {
            return Json(new { ok = true, dataobject = await Task.Run(() => _menu.GetByDefault(this.TenantId, 10)) }, JsonRequestBehavior.AllowGet);
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
                        
            _orderCommand.TenantId = this.TenantId;
            _orderCommand.OrderedSummary = entityToCreate;
            _orderCommand.CreatedBy = this.UserName;
            _orderCommand.CreatedDt = DateTime.UtcNow;
            _orderCommand.Execute();                        

            return Json(new { ok = true});
        }       

    }
}

