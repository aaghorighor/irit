namespace Suftnet.Cos.Web.Command
{
    using ViewModel;

    public interface IDashboardCommand
    {
        int TenantId { get; set; }
        DashboardModel Execute();
    }
}