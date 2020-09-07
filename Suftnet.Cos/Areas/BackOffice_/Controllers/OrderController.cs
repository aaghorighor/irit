namespace Suftnet.Cos.BackOffice
{
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Web.ActionFilter;
    using Suftnet.Cos.Extension;
    using Web;
    using System.Threading.Tasks;
    using Service;

    public class OrderController : FrontOfficeBaseController
    {
        #region Resolving dependencies

        private readonly IOrder _order;
        private readonly ITable _table;
        private readonly ICategory _category;
        private readonly IMenu _menu;
        private readonly ITenantCommon _common;

        public OrderController(IOrder order, ITenantCommon common,
            ITable table, ICategory category, IMenu menu)
        {
            _order = order;
            _table = table;
            _category = category;
            _menu = menu;
            _common = common;
        }

        #endregion

        public ActionResult Index()
        {           
            return View();
        }

        public async Task<JsonResult> Init(JQueryDataTableParamModel param)
        {
            IEnumerable<string[]> result = null;

            if (!string.IsNullOrEmpty(param.sSearch))
            {
                result = await System.Threading.Tasks.Task.Run(() => (from n in _order.GetAll(param.iDisplayStart, param.iDisplayLength, param.sSearch)
                                                                      orderby n.Id descending
                                                                      select new string[] { n.Id.ToString(), n.CreatedOn, n.CreatedBy, n.OrderType, n.FirstName, n.Time, n.Table == "" ? "0" : n.Table, n.NumberOfGuest != null ? n.NumberOfGuest.ToString() : "0", n.Status, n.GrandTotal.ToCurrency(), n.Payment.ToCurrency(), n.Balance.DecimalNullToCurrency(), "Actions" }));
            }
            else
            {
                result = await System.Threading.Tasks.Task.Run(() => (from n in _order.GetAll(param.iDisplayStart, param.iDisplayLength)
                                                                      orderby n.Id descending
                                                                      select new string[] { n.Id.ToString(), n.CreatedOn, n.CreatedBy, n.OrderType, n.FirstName, n.Time, n.Table == "" ? "0" : n.Table, n.NumberOfGuest != null ? n.NumberOfGuest.ToString() : "0", n.Status, n.GrandTotal.ToCurrency(), n.Payment.ToCurrency(), n.Balance.DecimalNullToCurrency(), "Actions" }));
            }

            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = result.Count(),
                iTotalDisplayRecords = _order.Count(),
                aaData = result
            },
                      JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetMenuByCategoryId(int Id)
        {
            try
            {
                Ensure.Argument.NotNull(Id);

                var model = await System.Threading.Tasks.Task.Run(() => _menu.GetByCategoryId(Id));
                return Json(new { ok = true, dataobject = model }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetMenuByMenuId(int Id)
        {
            try
            {
                Ensure.Argument.NotNull(Id);

                var model = await System.Threading.Tasks.Task.Run(() => _menu.Get(Id));
                return Json(new { ok = true, dataobject = model }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetMenus()
        {
            try
            {
                var model = await System.Threading.Tasks.Task.Run(() => _menu.GetAll());
                return Json(new { ok = true, dataobject = model }, JsonRequestBehavior.AllowGet);              
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetCategoryById()
        {
            try
            {
                var model = await System.Threading.Tasks.Task.Run(() => _category.GetByStatus(true));
                return Json(new { ok = true, dataobject = model }, JsonRequestBehavior.AllowGet);                
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetCommonById(int Id)
        {
            try
            {
                Ensure.Argument.NotNull(Id);

                var model = await System.Threading.Tasks.Task.Run(() => _common.Get(Id));
                return Json(new { ok = true, dataobject = model }, JsonRequestBehavior.AllowGet);               
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetTableById()
        {
            try
            {
                var model = await System.Threading.Tasks.Task.Run(() => _table.GetByStatus(true));
                return Json(new { ok = true, dataobject = model }, JsonRequestBehavior.AllowGet);          
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetOrderByReservation()
        {
            try
            {
                var model = await System.Threading.Tasks.Task.Run(() => _order.GetAllReserveOrder((int)OrderStatus.Reserved));
                return Json(new { ok = true, dataobject = model }, JsonRequestBehavior.AllowGet);              
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetOrderByDelivery()
        {
            try
            {
                var model = await System.Threading.Tasks.Task.Run(() => _order.GetDeliverys((int)OrderType.Delivery, (int)OrderStatus.Complete));
                return Json(new { ok = true, dataobject = model }, JsonRequestBehavior.AllowGet);               
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [HttpGet]
        public async Task<JsonResult> Get(int Id)
        {
            try
            {
                Ensure.Argument.NotNull(Id);

                var model = await System.Threading.Tasks.Task.Run(() => _order.Get(Id));
                return Json(new { ok = true, dataobject = model }, JsonRequestBehavior.AllowGet);              
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }              

        [HttpPost]
        //[PermissionFilter(WebPageType.Order, PermissionType.Create)]
        public JsonResult Create(OrderDto entityToCreate)
        {
            try
            {
                Ensure.Argument.NotNull(entityToCreate);

                if (entityToCreate == null)
                {
                    return Json(new { ok = false, msg = Constant.ValidationErrorMessage }, JsonRequestBehavior.AllowGet);
                } 
                   
                    entityToCreate.CreatedBy = this.UserName;
                    entityToCreate.TenantId = this.TenantId;
                    entityToCreate.CreatedDT = DateTime.UtcNow;

                if (entityToCreate.OrderTypeId ==(int)OrderType.Reservation)
                {
                    entityToCreate.TableId = entityToCreate.ReservationTableId;
                    entityToCreate.Time = entityToCreate.ReservationTime;
                    entityToCreate.CreatedDT = entityToCreate.ReservationDate;
                }

                if (entityToCreate.OrderTypeId == (int)OrderType.Delivery)
                {
                    entityToCreate.Time = entityToCreate.DeliveryTime;
                    entityToCreate.CreatedDT = entityToCreate.DeliveryDate;
                }

                if (entityToCreate.DeliveryId > 0)
                {
                    entityToCreate.Id = entityToCreate.DeliveryId;
                }

                if (entityToCreate.ReservationId > 0) 
                {
                    entityToCreate.Id = entityToCreate.ReservationId;
                }

               if (entityToCreate.Id == 0)
                {
                    entityToCreate.Reference = "".UniqueId();
                    entityToCreate.Id = _order.Insert(entityToCreate);
                    entityToCreate.flag = (int)flag.Add;
                }
                else
                {
                    _order.Update(entityToCreate);
                    entityToCreate.flag = (int)flag.Update;
                }

                if(entityToCreate.OrderTypeId !=(int)OrderType.Delivery)
                {
                    if (entityToCreate.StatusId == (int)OrderStatus.Occupied)
                    {
                        _table.UpdateStatus((int)TabelStatus.Occupied, entityToCreate.TableId, entityToCreate.Id);
                    }
                    else
                    {
                        _table.UpdateStatus((int)TabelStatus.Free, entityToCreate.TableId, 0);
                    }
                }               

                return Json(new { ok = true, flag = entityToCreate.flag, objrow = _order.Get(entityToCreate.Id) }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [HttpPost]
        public JsonResult ChangeDeliveryStatus(int orderId, int statusId)
        {
            try
            {
                _order.UpdateOrderStatus(orderId, statusId, (int)OrderType.Delivery, DateTime.UtcNow, this.UserName);

                return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }
         
        [HttpPost]      
        public JsonResult ChangeStatus(int orderId, int statusId, int tableId)
        {
            try
            {
                if(_order.UpdateOrderStatus(orderId, statusId,(int)OrderType.DineIn, DateTime.UtcNow, this.UserName))
                {
                    _table.UpdateStatus((int)TabelStatus.Occupied, tableId, orderId);
                }
            
                return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [HttpPost]
        //[PermissionFilter(WebPageType.Order, PermissionType.Remove)]
        public JsonResult Delete(int Id)
        {
            try
            {
                Ensure.Argument.NotNull(Id);

                return Json(new { ok = _order.Delete(Id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }
    }
}



