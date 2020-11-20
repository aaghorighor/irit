namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using System;
    using System.Threading.Tasks;
    using ViewModel;

    public interface IDashboardCommand
    {
        Guid TenantId { get; set; }
        DashboardModel Execute();
    }

    public interface IAdminDashboardCommand
    {
        AdminDashboardModel Execute();
    }

    public interface IBoostrapCommand
    {
        Guid TenantId { get; set; }
        MobileTenantDto User { get; set; }
        Task<BoostrapModel> Execute();
    }

    public interface IItemCommand
    {
        Guid TenantId { get; set; }      
        Task<BoostrapModel> Execute();
    }
}