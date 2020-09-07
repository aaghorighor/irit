namespace Suftnet.Cos.BackOffice
{
    using System;
    using System.Web.Mvc;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Core;
    using Suftnet.Cos.Common;
    using Web.ActionFilter;
    using Suftnet.Cos.Extension;
    using Web.Command;
    using Service;

    public class OrderDetailController : FrontOfficeBaseController
    {
        #region Resolving dependencies

        private readonly lOrderDetail _orderDetail;
        private readonly IOrder _order;
        private readonly IOrderCommand _orderCommand;
        private readonly ICommon _common;
        private readonly ITable _table;

        public OrderDetailController(IOrderCommand orderCommand, 
            lOrderDetail orderDetail,
            IOrder order, ITable table,
            ICommon common)
        {
            _orderCommand = orderCommand;
            _orderDetail = orderDetail;
            _order = order;         
            _common = common;
            _table = table;
        }

        #endregion

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult Get(int Id)
        {
            try
            {
                Ensure.Argument.NotNull(Id);

                return Json(new { ok = true, dataobject = _orderDetail.Get(Id) }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [HttpGet]
        public JsonResult GetOrderDetailById(int Id)
        {
            try
            {
                Ensure.Argument.NotNull(Id);

                var orderDetail = new OrderDetailWrapperDto
                {
                    PaymentMethod = _common.GetAll((int)eSettings.PaymentMethod),
                    OrderStatus = _common.GetAll((int)eSettings.OrderStatus),
                    Order = _order.Get(Id),
                    OrderDetail = _orderDetail.GetAll(Id)
                };

                return Json(new { ok = true, dataobject = orderDetail }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [HttpPost]
        //[PermissionFilter(WebPageType.Order, PermissionType.Create)]
        public JsonResult Create(OrderedSummaryDto entityToCreate)
        {
            try
            {
                Ensure.Argument.NotNull(entityToCreate);

                if (entityToCreate == null)
                {
                    return Json(new { ok = false, msg = Constant.ValidationErrorMessage }, JsonRequestBehavior.AllowGet);
                }

                if (entityToCreate.orderedItems.Count == 0)
                {
                    return Json(new { ok = false, msg = Constant.ValidationErrorMessage }, JsonRequestBehavior.AllowGet);
                }

                if (entityToCreate.OrderTypeId == (int)OrderType.Bar || entityToCreate.OrderTypeId == (int)OrderType.Takeway)
                {
                    var order = _order.Get(entityToCreate.OrderId);

                    if (order == null)
                    {
                        entityToCreate.OrderId = this.CreateOrder(entityToCreate.TableId, entityToCreate.OrderTypeId, entityToCreate.OrderStatusId);
                    }
                }

                if (entityToCreate.OrderStatusId == (int)OrderStatus.Complete)
                {
                   if(entityToCreate.AmountPaid < entityToCreate.Balance)
                    {
                        return Json(new { ok = false, msg = Constant.CompletedOrder }, JsonRequestBehavior.AllowGet);
                    }                       
                }

                _orderCommand.PaymentMethodId = entityToCreate.PaymentMethodId;
                _orderCommand.OrderId = entityToCreate.OrderId;
                _orderCommand.OrderStatusId = entityToCreate.OrderStatusId;

                _orderCommand.DiscountRate = entityToCreate.DiscountRate;
                _orderCommand.TotalDiscount = entityToCreate.TotalDiscount;

                _orderCommand.TaxRate = entityToCreate.TaxRate;
                _orderCommand.TotalTax = entityToCreate.TotalTax;
                _orderCommand.DeliveryCost = entityToCreate.DeliveryCost;

                _orderCommand.Note = entityToCreate.Note;
              
                _orderCommand.AmountPaid = entityToCreate.AmountPaid;              
                _orderCommand.ItemsOrdered = entityToCreate.orderedItems;

                _orderCommand.CreatedBy = this.UserName;
                _orderCommand.CreatedDt = DateTime.UtcNow;
                _orderCommand.Execute(); //// process order    

                if(entityToCreate.OrderTypeId !=(int)OrderType.Delivery)
                {
                    if (entityToCreate.OrderStatusId == (int)OrderStatus.Occupied)
                    {
                        _table.UpdateStatus((int)TabelStatus.Occupied, entityToCreate.TableId, entityToCreate.OrderId);
                    }
                    else
                    {
                        _table.UpdateStatus((int)TabelStatus.Free, entityToCreate.TableId, 0);
                    }
                }               

                var receipt =  new OrderDetailWrapperDto
                {
                    IsKitchenReceipt = entityToCreate.IsKitchenReceipt,
                    IsPrintReceipt = entityToCreate.IsPrintReceipt,
                    OrderId = entityToCreate.OrderId,
                    Settings = GeneralConfiguration.Configuration.Settings.General,
                    Order = _order.Get(entityToCreate.OrderId),
                    OrderDetail = _orderDetail.GetAll(entityToCreate.OrderId)
                };

                if (entityToCreate.IsPrintReceipt || entityToCreate.IsKitchenReceipt)
                {
                    PrintReceipt(entityToCreate.IpAddress, receipt);
                }              

                return Json(new { ok = true, receipt= receipt });     
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
                var orderDetails = _orderDetail.Get(Id);

                var order = _order.Get(orderDetails.OrderId);

                return Json(new { ok = _orderDetail.Delete(Id) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [HttpPost]
        public JsonResult TestPrint(string ipAddress)
        {
            try
            {
                if (!string.IsNullOrEmpty(ipAddress))
                {
                    //BellaHotelClient(ipAddress).TestPrint();
                }
                return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        [HttpPost]
        //[PermissionFilter(WebPageType.CashDrawer, PermissionType.Get)]
        public JsonResult OpenCashDrawer(string ipAddress)
        {
            try
            {
                if (!string.IsNullOrEmpty(ipAddress))
                {
                    //BellaHotelClient(ipAddress).OpenCashDrawer();
                }

                return Json(new { ok = true }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Logger(ex);
            }
        }

        #region private

        private void PrintReceipt(string ipAddress, OrderDetailWrapperDto receiptToPrint)
        {
            try
            {
                if (!string.IsNullOrEmpty(ipAddress))
                {
                    //BellaHotelClient(ipAddress).PrintReceipt(receiptToPrint);
                }
            }
            catch (Exception ex)
            {
                Logger(ex);
            }
        }
        private void BellaHotelClient(string ipAddress)
        {
           // return new BellaHotelClient(new Uri("http://" + ipAddress), "000", "000");
        }
        private int CreateOrder(int tableId, int orderTypeId, int statusId)
        {
            var order = new OrderDto
            {
                TableId = tableId,
                StatusId = statusId,
                Reference = "".UniqueId(),
                NumberOfGuest = 1,
                CreatedBy = this.UserName,
                CreatedDT = DateTime.UtcNow,
                OrderTypeId = orderTypeId,
                Time = DateTime.UtcNow.ToShortTimeString()
            };

          return  _order.Insert(order);
        }

        #endregion
    }
}

