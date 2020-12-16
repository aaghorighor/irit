namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Web.ViewModel;

    public interface ICreateOrderCommand
    {       
        CreateOrder entityToCreate { get; set; }
        void Execute();
   }

    public interface IUpdateOrderCommand
    {
        OrderAdapter OrderAdapter { get; set; }
        void Execute();
    }
}
