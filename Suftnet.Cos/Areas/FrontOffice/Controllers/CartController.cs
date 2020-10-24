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
        private readonly ITable _table;
        private readonly IOrderCommand _orderCommand;

        public CartController(ICategory category, IMenu menu, IOrderCommand orderCommand,
          IOrder order, ITable table)
        {           
            _menu = menu;
            _category = category;
            _order = order;
            _table = table;
            _orderCommand = orderCommand;
        }
        #endregion
        [OutputCache(Duration = 0, VaryByParam = "*")]
        public ActionResult Entry(string orderId, string orderTypeId, string orderType, string orderStatusId)
        {
            return View();
        }

        [HttpGet]
        public JsonResult FetchCart(Guid orderId)
        {
            return Json(new { ok = true, dataobject = _order.Get(orderId) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FetchCategories()
        {
            return Json(new { ok = true, dataobject = _category.GetByStatus(true, this.TenantId) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FetchDefaultMenues()
        {
            return Json(new { ok = true, dataobject = _menu.GetByDefault(this.TenantId, 10) }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult FetchMenuByCategory(Guid categoryId)
        {
            return Json(new { ok = true, dataobject = _menu.GetByCategoryId(categoryId, this.TenantId) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]       
        [ValidateHeaderAntiForgeryToken]
        public JsonResult Create(OrderedSummaryDto entityToCreate)
        {
            Ensure.Argument.NotNull(entityToCreate);

            if (entityToCreate.OrderStatusId == new Guid(eOrderStatus.Completed.ToLower()))
            {
                if (entityToCreate.AmountPaid < entityToCreate.Balance)
                {
                    return Json(new { ok = false, msg = Constant.CompletedOrder }, JsonRequestBehavior.AllowGet);
                }
            }

            _orderCommand.TenantId = this.TenantId;
            _orderCommand.OrderedSummary = entityToCreate;
            _orderCommand.CreatedBy = this.UserName;
            _orderCommand.CreatedDt = DateTime.UtcNow;
            _orderCommand.Execute(); 

            if (entityToCreate.OrderTypeId == new Guid(eOrderType.DineIn.ToLower()))
            {
                if (entityToCreate.OrderStatusId == new Guid(eOrderStatus.Completed.ToLower()))
                {
                    ResetTable(entityToCreate);
                }
            }

            return Json(new { ok = true});
        }

        #region Private function

        private void ResetTable(OrderedSummaryDto entityToCreate)
        {
            var param = new TableDto
            {
                  Id = entityToCreate.TableId,                 
                  UpdateBy = this.UserName,
                  UpdateDate = DateTime.UtcNow
            };

            try
            {
                Task.Run(() => _table.Reset(param));
            }
            catch(Exception ex)
            {
                GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>().LogError(ex);
            }           
        }

        #endregion

    }
}

