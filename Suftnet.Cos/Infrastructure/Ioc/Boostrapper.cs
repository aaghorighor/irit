namespace Suftnet.Cos.Web
{ 
    using Core;  
    using StructureMap;

    using Suftnet.Cos.DataAccess;
    using Suftnet.Cos.Stripe;

    using System.Configuration;

    public static class Boostrapper
    {
        public static IContainer Start()
        {
            ObjectFactory.Configure(x =>
            {
                x.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.LookForRegistries();
                });
                          
                //// Section for adding registry classes
                x.AddRegistry(new StripeRegistry(""));                
                x.AddRegistry(new CoreRegistry());                           
                x.AddRegistry(new DataAccessSystemRegistry());
                x.AddRegistry(new DataAccessBackOfficeRegistry(0));

            });

            return ObjectFactory.Container;
        }
    }
}
