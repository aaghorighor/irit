namespace Suftnet.Cos.FrontOffice
{
    using System;
    using System.Web.Mvc;

    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.CommonController.Controllers;
    using Suftnet.Cos.Extension;
    using Service;
    using System.Linq;
    using Suftnet.Cos.Web;
    using System.Threading.Tasks;
    using Suftnet.Cos.Web.Infrastructure.ActionFilter;

    public class DineInController : FrontOfficeBaseController
    {     
        #region Resolving dependencies

        private readonly IOrder _order;
        private readonly ITable _table;

        public DineInController(IOrder order, ITable table)
        {
            _order = order;
            _table = table;
        }
        #endregion
        [OutputCache(Duration = 10, VaryByParam = "*")]
        public virtual ActionResult Index()
        {           
            return View();         
        }
        public virtual async Task<JsonResult> Fetch(DataTableAjaxPostModel param)
        {
            var model = await Task.Run(() => _order.GetAll(new Guid(eOrderType.DineIn),this.TenantId, param.start, param.length, param.search.value));

            return Json(new
            {
                draw = param.draw,
                recordsTotal = model.Count(),
                recordsFiltered = _order.CountByOrderType(this.TenantId,new Guid(eOrderType.DineIn)),
                data = model
            },
                      JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PermissionFilter(BackOfficeViews.Member, PermissionType.Create)]
        [ValidateAntiForgeryToken]
        public virtual JsonResult Create(OrderDto entityToCreate)
        {
            Ensure.Argument.NotNull(entityToCreate);

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    ok = false,
                    isValid = true,
                    errors = ModelState.AjaxErrors()
                });
            }

            entityToCreate.CreatedDT = DateTime.UtcNow;
            entityToCreate.CreatedBy = this.UserName;

            entityToCreate.UpdateDate = DateTime.UtcNow;
            entityToCreate.UpdateBy = this.UserName;

            entityToCreate.StartDt = DateTime.UtcNow;          

            entityToCreate.OrderTypeId = new Guid(eOrderType.DineIn);
            entityToCreate.StatusId = new Guid(eOrderStatus.Occupied);

            entityToCreate.TenantId = this.TenantId;
            entityToCreate.Id = Guid.NewGuid();

            entityToCreate.FirstName = string.Empty;
            entityToCreate.LastName = string.Empty;

            _order.Insert(entityToCreate);
            entityToCreate.flag = (int)flag.Add;

            SetOrderTable(entityToCreate);

            return Json(new { ok = true, order = new {orderId = entityToCreate.Id, tableId= entityToCreate.TableId, orderStatusId = entityToCreate.StatusId, orderType = entityToCreate.OrderTypeId }, flag = entityToCreate.flag }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PermissionFilter(BackOfficeViews.Member, PermissionType.Create)]
        [ValidateHeaderAntiForgeryTokenAttribute]
        public virtual JsonResult Edit(OrderDto entityToCreate)
        {
            Ensure.Argument.NotNull(entityToCreate);

            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    ok = false,
                    isValid = true,
                    errors = ModelState.AjaxErrors()
                });
            }

            entityToCreate.FirstName = string.Empty;
            entityToCreate.LastName = string.Empty;

            entityToCreate.UpdateDate = DateTime.UtcNow;
            entityToCreate.UpdateBy = this.UserName;
                      
            _order.Update(entityToCreate);
            entityToCreate.flag = (int)flag.Update;

            if(entityToCreate.ChangeTable)
            {
                SetOrderTable(entityToCreate);
            }
                 
            return Json(new { ok = true, flag = entityToCreate.flag }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PermissionFilter(BackOfficeViews.Member, PermissionType.Remove)]
        public virtual JsonResult Delete(string Id)
        {
            Ensure.NotNull(Id);
            return Json(new { ok = _order.Delete( new Guid(Id)) }, JsonRequestBehavior.AllowGet);
        }

        #region private function
        private void SetOrderTable(OrderDto entityToCreate)
        {
            if (entityToCreate.StatusId == new Guid(eOrderStatus.Occupied))
            {
                Task.Run(() => _table.UpdateStatus(entityToCreate.StatusId, entityToCreate.TableId, entityToCreate.Id, DateTime.UtcNow, this.UserName));
            }
        }
        #endregion

    }
}

