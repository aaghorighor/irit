namespace Suftnet.Cos.Web.Command
{
    public interface IFactoryCommand
    {
        T Create<T>() where T : ICommand;
    }
}
