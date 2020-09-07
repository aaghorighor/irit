namespace Suftnet.Cos.Web.Command
{
    using Suftnet.Cos.Core;

    public class FactoryCommand : IFactoryCommand
    {
        public T Create<T>() where T : ICommand
        {
            return GeneralConfiguration.Configuration.DependencyResolver.GetService<T>();
        }
    }
}