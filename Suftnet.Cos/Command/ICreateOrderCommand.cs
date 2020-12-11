namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.DataAccess;
   
    public interface ICreateOrderCommand
    {       
        CreateOrder entityToCreate { get; set; }
        void Execute();
   }
}
