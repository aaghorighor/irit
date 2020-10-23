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
   
    public class DeliveryController : FrontOfficeBaseController
    {     
        #region Resolving dependencies

        private readonly IOrder _order;
        private readonly IDeliveryAddress _deliveryAddress;

        public DeliveryController(IOrder order, IDeliveryAddress deliveryAddress) 
        {
            _order = order;
            _deliveryAddress = deliveryAddress;
        }

        #endregion      
        [OutputCache(Duration = 0, VaryByParam = "*")]
        public virtual ActionResult Index()
        {
            return View();
        }
        public JsonResult Fetch(DataTableAjaxPostModel param)
        {
            var model = _order.GetDeliveryOrders(new Guid(eOrderType.Delivery), this.TenantId, param.start, param.length, param.search.value);

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
        [PermissionFilter(BackOfficeViews.Member, PermissionType.Create)]
        [ValidateAntiForgeryToken]
        public JsonResult Create(DeliveryAddressDto entityToCreate)
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

            entityToCreate.OrderTypeId = new Guid(eOrderType.Delivery);
            entityToCreate.StatusId = new Guid(eOrderStatus.Pending);

            entityToCreate.TenantId = this.TenantId;
            entityToCreate.Id = Guid.NewGuid();

            _order.Insert(entityToCreate);

            entityToCreate.DeliveryId = Guid.NewGuid();
            entityToCreate.OrderId = entityToCreate.Id;
            entityToCreate.CreatedAt = DateTime.UtcNow;

            _deliveryAddress.Insert(entityToCreate);

            entityToCreate.flag = (int)flag.Add;
          
            return Json(new { ok = true, order = new { orderId = entityToCreate.Id, tableId = entityToCreate.TableId, orderStatusId = entityToCreate.StatusId, orderType = entityToCreate.OrderTypeId }, flag = entityToCreate.flag }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PermissionFilter(BackOfficeViews.Member, PermissionType.Create)]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(DeliveryAddressDto entityToCreate)
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

            entityToCreate.UpdateDate = DateTime.UtcNow;
            entityToCreate.UpdateBy = this.UserName;

            _order.UpdateDelivery(entityToCreate);
            _deliveryAddress.Update(entityToCreate);

            entityToCreate.flag = (int)flag.Update;
           
            return Json(new { ok = true, flag = entityToCreate.flag }, JsonRequestBehavior.AllowGet);
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

