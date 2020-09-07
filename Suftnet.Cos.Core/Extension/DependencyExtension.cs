namespace Suftnet.Cos.Core
{
    using System.Collections.Generic;
    using System.Linq;   
    using System.Web.Http.Dependencies;
    
    public static class DependencyResolverExtensions
    {
        public static TService GetService<TService>(this IDependencyResolver resolver)
        {
            return (TService)resolver.GetService(typeof(TService));
        }

        public static IEnumerable<TService> GetServices<TService>(this IDependencyResolver resolver)
        {
            return resolver.GetServices(typeof(TService)).Cast<TService>();
        }        
    }
}
