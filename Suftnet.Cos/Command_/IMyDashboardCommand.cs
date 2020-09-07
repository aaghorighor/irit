namespace Suftnet.Cos.Web.Command
{
    using ViewModel;

    public interface IMyDashboardCommand
    {
        int MemberId { get; set; }
        MyDashboardModel Execute();
    }
}