namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using ViewModel;
    using Common;
    using System;
    using System.Threading.Tasks;
    
    public class DashboardCommand : IDashboardCommand
    {      
        private readonly ITable _table;
        private readonly IOrder _order;
       
        public DashboardCommand(
            ITable table,
            IOrder order)
        {
            _table = table;
            _order = order;            
        }               
              
        public Guid TenantId { get; set; }

        public DashboardModel Execute()
        {
            return  DashboardAsync();
        }

        #region private function
        public DashboardModel DashboardAsync()
        {
            var continuation = Task.WhenAll(
                Task.Run(() => _table.GetFreeCount(TenantId)),
                Task.Run(() => _order.Count(new Guid(eOrderStatus.Reserved), TenantId, new Guid(eOrderType.Reservation))),
                Task.Run(() => _order.Count(TenantId, new Guid(eOrderType.DineIn), new Guid(eOrderStatus.Pending), new Guid(eOrderStatus.Processing), new Guid(eOrderStatus.Ready))),
                Task.Run(() => _order.Count(new Guid(eOrderStatus.Occupied), TenantId, new Guid(eOrderType.DineIn))));                
            try
            {
                continuation.Wait();
            }
            catch (AggregateException)
            { }

            if(continuation.IsCompleted)
            {
                var test = new DashboardModel
                {
                    FreeTables = continuation.Result[0],
                    Reservations = continuation.Result[1],
                    PendingDeliveries = continuation.Result[2],
                    DineIn = continuation.Result[3]                    
                };

                return test;
            }
            return new DashboardModel { };
        }             

        #endregion

    }
}