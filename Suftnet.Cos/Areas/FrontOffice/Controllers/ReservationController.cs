﻿namespace Suftnet.Cos.FrontOffice
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

    public class ReservationController : FrontOfficeBaseController
    {     
        #region Resolving dependencies

        private readonly IOrder _order;
        private readonly ITable _table;

        public ReservationController(IOrder order, ITable table) 
        {
            _order = order;
            _table = table;
        }

        #endregion       
        public JsonResult Fetch(DataTableAjaxPostModel param)
        {
            var model = _order.GetReserveOrders(new Guid(eOrderType.Reservation), new Guid(eOrderStatus.Reservation), this.TenantId, param.start, param.length, param.search.value);

            return Json(new
            {
                draw = param.draw,
                recordsTotal = model.Count(),
                recordsFiltered = _order.Count(new Guid(eOrderType.Reservation),new Guid(eOrderStatus.Reservation), this.TenantId),
                data = model
            },
                      JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PermissionFilter(BackOfficeViews.Member, PermissionType.Create)]
        [ValidateAntiForgeryToken]
        public JsonResult Create(OrderDto entityToCreate)
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

            Map(entityToCreate);

            entityToCreate.CreatedDT = DateTime.UtcNow;
            entityToCreate.CreatedBy = this.UserName;

            entityToCreate.UpdateDate = DateTime.UtcNow;
            entityToCreate.UpdateBy = this.UserName;

            entityToCreate.OrderTypeId = new Guid(eOrderType.Reservation);
           
            entityToCreate.TenantId = this.TenantId;
            entityToCreate.Id = Guid.NewGuid();

            _order.Insert(entityToCreate);
            entityToCreate.flag = (int)flag.Add;
          
            return Json(new { ok = true, flag = entityToCreate.flag }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [PermissionFilter(BackOfficeViews.Member, PermissionType.Edit)]
        [ValidateAntiForgeryToken]
        public JsonResult Edit(OrderDto entityToCreate)
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

            Map(entityToCreate);

            entityToCreate.UpdateDate = DateTime.UtcNow;
            entityToCreate.UpdateBy = this.UserName;
                      
            _order.UpdateReserve(entityToCreate);
            entityToCreate.flag = (int)flag.Update;

            return Json(new { ok = true, flag = entityToCreate.flag }, JsonRequestBehavior.AllowGet);
        }

        private void Map(OrderDto entityToCreate)
        {
            entityToCreate.FirstName = entityToCreate.ReservationFirstName;
            entityToCreate.LastName = entityToCreate.ReservationLastName;
            entityToCreate.StatusId = entityToCreate.ReservationStatusId;
            entityToCreate.TableId = entityToCreate.ReservationTableId;
            entityToCreate.Time = entityToCreate.ReservationTime;
            entityToCreate.ExpectedGuest = entityToCreate.ReservationExpectedGuest;
            entityToCreate.Id = entityToCreate.ReservationId;
           
        }

    }
}
