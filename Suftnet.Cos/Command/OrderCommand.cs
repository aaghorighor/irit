namespace Suftnet.Cos.Web.Command
{
    using System;
    using System.Collections.Generic;
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Common;
    using Suftnet.Cos.Core;
    using System.Threading.Tasks;

    public class OrderCommand : IOrderCommand
    {
       private readonly IOrderDetail _cart;
       private readonly IOrder _order;
       private readonly IOrderPayment _orderPayment;   
       private readonly IMenu _menu;
       private readonly IPayment _payment;
       private readonly ITable _table;

        public OrderCommand(
           IPayment payment, IOrderPayment
           orderPayment, ITable table, IOrder order, IMenu menu, IOrderDetail cart)
       {
           _cart = cart;
           _order = order;
           _payment = payment;
           _orderPayment = orderPayment;
           _menu = menu;
           _table = table;
       }

        public void Execute()
        {
            decimal total = 0m;                          
            bool isProcessing = true;
            var order = new OrderDto();

            if(this.OrderTypeId == new Guid(eOrderType.Delivery.ToLower()))
            {
                order = _order.FetchDeliveryOrder(this.OrderId);
            }else
            {
                order = _order.Get(this.OrderId);
            }         

            var totalPayment = _orderPayment.GetTotalPaymentByOrderId(this.OrderId);                 

            if (order != null)
            {
                _cart.ClearOrderDetailByOrderId(this.OrderId);

                foreach (var item in ItemsOrdered)
                {
                    var lineTotal = 0m;
                    bool? isKitchen = false;

                    var menu = _menu.Get(item.MenuId);
                       
                        if (menu != null)
                        {
                            lineTotal = (item.Price);
                            total += item.Price;

                        if (order.OrderTypeId == new Guid(eOrderType.Reservation.ToLower()))
                        {
                            isProcessing = false;                       
                        } 
                        
                        if(menu.IsKitchen != null)
                        {
                            isKitchen = (bool)menu.IsKitchen;
                        }

                        var orderItem = new OrderDetailDto()
                        {
                            IsKitchen = isKitchen,
                            TaxRate = 0,
                            Quantity = 1,
                            Price = item.Price,
                            IsProcessed = isProcessing,
                            ItemName = item.Name,
                            MenuId = item.MenuId,
                            OrderId = OrderId,
                            Discount = 0,
                            Total = lineTotal,
                            AddonIds = item.AddonIds,                          
                            AddonNames = item.AddonNames,
                            Id = Guid.NewGuid(),

                            CreatedBy = CreatedBy,
                            CreatedDT = CreatedDt                            
                        };

                        _cart.Insert(orderItem);

                        if (order.OrderTypeId != new Guid(eOrderType.Reservation.ToLower()))
                        {
                            if (order.StatusId != new Guid(eOrderStatus.Completed.ToLower()))
                            {
                                if (menu.SubStractId == SubStract.Yes)
                                {
                                    _menu.UpdateMenuQuantity(1, item.MenuId);
                                }
                            }
                        }
                    }
                  }              

                if (this.AmountPaid > 0 )
                {
                    order.Payment = totalPayment + this.AmountPaid;

                    var paymentId = _payment.Insert(
                        new PaymentDto
                    {
                        Amount = (decimal)order.Payment,                        
                        Reference = order.Id.ToString(),
                        TenantId = this.TenantId,
                        PaymentMethodId = PaymentMethodId,
                        Id = Guid.NewGuid(),

                        CreatedBy = CreatedBy,
                        CreatedDT = CreatedDt,
                        UpdateDate = CreatedDt,
                        UpdateBy = CreatedBy });

                   _orderPayment.Insert(
                       new OrderPaymentDto
                   {
                       OrderId = this.OrderId,
                       PaymentId = paymentId,

                       Id = Guid.NewGuid(),
                       CreatedDT = CreatedDt,
                       CreatedBy = CreatedBy
                   });     
                }

                if (order.OrderTypeId == new Guid(eOrderType.DineIn.ToLower()))
                {
                    if (this.OrderStatusId == new Guid(eOrderStatus.Completed.ToLower()))
                    {
                        ResetTableOrder(order);
                    }
                }

                order.TotalDiscount =TotalDiscount;
                order.TotalTax = TotalTax;
                order.TaxRate = TaxRate;
                order.DiscountRate = DiscountRate;
                order.Total = total;
                order.GrandTotal = (total + DeliveryCost + TotalTax) - TotalDiscount;
                order.StatusId = this.OrderStatusId;              
                order.Note = this.Note;              
                order.Balance = Util.Balance(order.GrandTotal, order.Payment);

                order.UpdateDate = CreatedDt;
                order.UpdateBy = CreatedBy;

               _order.UpdateSalesOrder(order);

            }
        }
       
        private Guid PaymentMethodId {
            get {
                return this.OrderedSummary.PaymentMethodId;
            } 
        }
        private decimal AmountPaid
        {
            get
            {
                return this.OrderedSummary.AmountPaid;
            }
        }
        private Guid OrderStatusId
        {
            get
            {
                return this.OrderedSummary.OrderStatusId;
            }
        }
        private Guid OrderTypeId
        {
            get
            {
                return this.OrderedSummary.OrderTypeId;
            }
        }
        public Guid TenantId { get; set; }
       
        private decimal TotalDiscount
        {
            get
            {
                return this.OrderedSummary.TotalDiscount;
            }
        }
        private decimal DiscountRate
        {
            get
            {
                return this.OrderedSummary.DiscountRate;
            }
        }
        private decimal TotalTax
        {
            get
            {
                return this.OrderedSummary.TotalTax;
            }
        }
        private decimal TaxRate
        {
            get
            {
                return this.OrderedSummary.TaxRate;
            }
        }
        private decimal DeliveryCost
        {
            get
            {
                return this.OrderedSummary.DeliveryCost;
            }
        }       
        private string Note
        {
            get
            {
                return this.OrderedSummary.Note;
            }
        }
        private Guid OrderId
        {
            get
            {
                return this.OrderedSummary.OrderId;
            }
        }
        public DateTime CreatedDt { get; set; }
        public string CreatedBy { get; set; }
        private List<OrderedItemDto> ItemsOrdered
        {
            get
            {
                return this.OrderedSummary.orderedItems;
            }
        }
        public OrderedSummaryDto OrderedSummary { get; set; }

        private void ResetTableOrder(OrderDto order)
        {
            var param = new TableDto
            {
                Id = order.TableId,
                UpdateBy = this.CreatedBy,
                UpdateDate =this.CreatedDt
            };

            try
            {
                Task.Run(() => _table.Reset(param));
            }
            catch (Exception ex)
            {
                GeneralConfiguration.Configuration.DependencyResolver.GetService<ILogger>().LogError(ex);
            }
        }
    }
}
