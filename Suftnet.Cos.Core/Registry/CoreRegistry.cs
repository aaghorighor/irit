namespace Suftnet.Cos.Core
{
   using StructureMap.Configuration.DSL;  

   public class CoreRegistry : Registry
   {
       public CoreRegistry()
       {
            For<ICacheService>().Use<SimpleCacheService>();
            For<ILogger>().Use<LogAdapter>();
            For<ICrytoService>().Use<CryptoService>();
        }
   } 
}
