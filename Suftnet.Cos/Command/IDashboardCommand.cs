namespace Suftnet.Cos.Web.Command
{
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
        Task<BoostrapModel> Execute();
    }
}