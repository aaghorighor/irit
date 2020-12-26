namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;    
    using System;   
    using Suftnet.Cos.Common;
    using System.Threading.Tasks;
    using Suftnet.Cos.Extension;

    public class PaymentCommand : IPaymentCommand
    {
        private readonly IOrder _order;
        private readonly ITable _table;
        private readonly IOrderPayment _orderPayment;
        private readonly IPayment _payment;
        private decimal _amountPaid = 0;

        public PaymentCommand(
            IOrder order, ITable table, IPayment payment, IOrderPayment
           orderPayment)
        {
            _order = order;
            _table = table;
            _payment = payment;
            _orderPayment = orderPayment;
        }               
              
        public PaymentModel Model { get; set; }     

        public void Execute()
        {
            CreatePayment();
            ResetTable();
            UpdateOrder();
        }

        #region private function
        private void ResetTable()
        {
            _table.Reset(new TableDto
            {
                Id = new Guid(Model.TableId),
                TenantId = new Guid(Model.ExternalId),
                UpdateBy = Model.UserName,
                UpdateDate = Model.CreatedDt.ToDate()
            });
        }

        private void UpdateOrder()
        {
            _order.UpdatePayment(new OrderDto
            {
                Id = new Guid(Model.OrderId),
                Payment = _amountPaid,
                PaymentStatusId = new Guid(ePaymentStatus.Paid),
                Balance = Util.Balance(Model.GrandTotal, _amountPaid),
                UpdateBy = Model.UserName,
                UpdateDate = Model.CreatedDt.ToDate()
            });
        }

        private void CreatePayment()
        {
             var totalPayment = _orderPayment.GetTotalPaymentByOrderId(new Guid(Model.OrderId));
            _amountPaid = totalPayment + Model.AmountPaid;

                var paymentId = _payment.Insert(new PaymentDto
                {
                    Amount = _amountPaid,
                    Reference = Model.OrderId,
                    TenantId = new Guid(Model.ExternalId),
                    PaymentMethodId = new Guid(Model.PaymentMethodId),
                    Id = Guid.NewGuid(),

                    CreatedBy = Model.UserName,
                    CreatedDT = Model.CreatedDt.ToDate(),
                    UpdateDate = Model.CreatedDt.ToDate(),
                    UpdateBy = Model.UserName
                });

                 _orderPayment.Insert(new OrderPaymentDto
                {
                    OrderId = new Guid(Model.OrderId),
                    PaymentId = paymentId,
                    Id = Guid.NewGuid(),

                    CreatedBy = Model.UserName,
                    CreatedDT = Model.CreatedDt.ToDate(),
                    UpdateDate = Model.CreatedDt.ToDate(),
                    UpdateBy = Model.UserName
                 });
        }

        #endregion

    }
}