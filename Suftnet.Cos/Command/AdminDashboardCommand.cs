namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using ViewModel;
    using Common;
    using System;
    using System.Threading.Tasks;
    using Suftnet.Cos.Core;

    public class AdminDashboardCommand : IAdminDashboardCommand
    {      
        private readonly ITenant _tenant;
        private readonly IMobileLogger _mobileLogger;
        private readonly ILogViewer _logger;

        public AdminDashboardCommand(
            ITenant tenant, 
            IMobileLogger mobileLogger,
            ILogViewer logger)
        {           
            _tenant = tenant;
            _mobileLogger = mobileLogger;
            _logger = logger;
        }               
              
        public int TenantId { get; set; }

        public AdminDashboardModel Execute()
        {
            return  DashboardAsync();
        }

        #region private function
        public AdminDashboardModel DashboardAsync()
        {
            var continuation = Task.WhenAll(Task.Run(() => _tenant.Count()),
                Task.Run(() => _tenant.Status(new Guid(SubscriptionStatus.Active), new Guid(Constant.DEMO_TENANTID))),
                Task.Run(() => _tenant.Status(new Guid(SubscriptionStatus.Expired), new Guid(Constant.DEMO_TENANTID))),
                Task.Run(() => _tenant.Status(new Guid(SubscriptionStatus.Cancelled), new Guid(Constant.DEMO_TENANTID))),
                Task.Run(() => _tenant.Status(new Guid(SubscriptionStatus.Suspended), new Guid(Constant.DEMO_TENANTID))),
                Task.Run(() => _tenant.Status(new Guid(SubscriptionStatus.Trial), new Guid(Constant.DEMO_TENANTID))),          
                Task.Run(() => _mobileLogger.Count()),
                Task.Run(() => _logger.Count()));
            try
            {
                continuation.Wait();
            }
            catch (AggregateException ex)
            {
                GeneralConfiguration.Configuration.Logger.LogError(ex);
            }

            if(continuation.IsCompleted)
            {
                var test = new AdminDashboardModel
                {                        
                    Mobile  = continuation.Result[6],
                    Paid  = continuation.Result[1],
                    Tenants = continuation.Result[0],
                    Web = continuation.Result[7],
                    Cancelled = continuation.Result[3],
                    Expired = continuation.Result[2],
                    Trials = continuation.Result[5],
                    Suspended = continuation.Result[4]
                };

                return test;
            }
            return new AdminDashboardModel { };
        }             

        #endregion

    }
}