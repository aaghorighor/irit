
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
              
        public PaymentModel paymentModel { get; set; }     

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
                Id = new Guid(paymentModel.TableId),
                TenantId = new Guid(paymentModel.ExternalId),
                UpdateBy = paymentModel.UserName,
                UpdateDate = paymentModel.CreatedDt.ToDate()
            });
        }

        private void UpdateOrder()
        {
            _order.UpdatePayment(new OrderDto
            {
                Id = new Guid(paymentModel.OrderId),
                Payment = _amountPaid,
                PaymentStatusId = new Guid(ePaymentStatus.Paid),
                StatusId = new Guid(eOrderStatus.Completed),
                Balance = Util.Balance(paymentModel.GrandTotal, _amountPaid),
                UpdateBy = paymentModel.UserName,
                UpdateDate = paymentModel.CreatedDt.ToDate()
            });
        }

        private void CreatePayment()
        {
             var totalPayment = _orderPayment.GetTotalPaymentByOrderId(new Guid(paymentModel.OrderId));
            _amountPaid = totalPayment + paymentModel.AmountPaid;

             var paymentId = _payment.Insert(new PaymentDto
                {
                    Amount = _amountPaid,
                    Reference = paymentModel.OrderId,
                    TenantId = new Guid(paymentModel.ExternalId),
                    PaymentMethodId = new Guid(paymentModel.PaymentMethodId),
                    AccountTypeId = new Guid(paymentModel.AccountTypeId),
                    Id = Guid.NewGuid(),

                    CreatedBy = paymentModel.UserName,
                    CreatedDT = paymentModel.CreatedDt.ToDate(),
                    UpdateDate = paymentModel.CreatedDt.ToDate(),
                    UpdateBy = paymentModel.UserName
                });

            _orderPayment.Insert(new OrderPaymentDto
                {
                    OrderId = new Guid(paymentModel.OrderId),
                    PaymentId = paymentId,
                    Id = Guid.NewGuid(),

                    CreatedBy = paymentModel.UserName,
                    CreatedDT = paymentModel.CreatedDt.ToDate(),
                    UpdateDate = paymentModel.CreatedDt.ToDate(),
                    UpdateBy = paymentModel.UserName
                 });
        }

        #endregion

    }
}