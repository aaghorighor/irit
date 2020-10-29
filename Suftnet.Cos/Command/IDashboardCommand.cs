namespace Suftnet.Cos.Web.Command
{
    using System.Threading.Tasks;
    using ViewModel;

    public interface IDashboardCommand
    {
        int TenantId { get; set; }
        DashboardModel Execute();
    }

    public interface IAdminDashboardCommand
    {
        AdminDashboardModel Execute();
    }
}