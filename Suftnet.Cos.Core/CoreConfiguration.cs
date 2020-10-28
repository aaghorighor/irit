namespace Suftnet.Cos.Core
{
    using Core;
    using System;
    using System.Collections.Generic;
    using System.Web.Http.Dependencies;

    public class CoreConfiguration
   {
       public CoreConfiguration()
       {
           Settings = new Settings();          
           CryptoService = new CryptoService();
           CacheService = new SimpleCacheService();          
        }

       public IDependencyResolver DependencyResolver { get; set; }

       public ILogger Logger
       {
           get
           {
               return DependencyResolver.GetService<ILogger>();
           }
       }

       public Exception FirstError { get; set; }
       public bool DebugEnabled { get; set; }
       public bool Shutdown { get; set; }
       public bool FailedOnInitialize { get; set; }
       public string HosterName { get; set; }
       public int ExecutingContext { get; set; }
       public Bibles ASV { get; set; }
       public Bibles KJV { get; set; }
       public Settings Settings { get; set; }   
       public ICacheService CacheService { get; set; }
       public CryptoService CryptoService { get; set; }    
    }
}
