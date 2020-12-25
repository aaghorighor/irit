namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.Common;
    using Suftnet.Cos.DataAccess;    
    using System;
   
    public class CancelOrderCommand : ICancelOrderCommand
    {
        private readonly IOrder _order;
        private readonly ITable _table;

        public CancelOrderCommand(
            IOrder order, ITable table)
        {
            _order = order;
            _table = table;

        }

        public Guid OrderId { get; set; }
        public Guid TableId { get; set; }      
        public Guid TenantId { get; set; }
        public string UserName { get; set; }
        public DateTime UpdateDate { get; set; }

        public void Execute()
        {
            UpdateOrder();
            ResetTable();
        }

        #region private function
        private void UpdateOrder()
        {
            _order.CancelOrder(
                   OrderId,
                   new Guid(eOrderStatus.Cancelled),
                   new Guid(ePaymentStatus.Cancelled),
                   UpdateDate, UserName, TenantId);
        }

        private void ResetTable()
        {
            _table.Reset(new TableDto
            {
                Id = TableId,
                TenantId = TenantId,
                UpdateBy = UserName,
                UpdateDate = UpdateDate
            });
        }
        #endregion

    }
}