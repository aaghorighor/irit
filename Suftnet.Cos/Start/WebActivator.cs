namespace Suftnet.Cos.Web
{
    using StructureMap; 
  
    public static class WebActivator
    {       
        public static void Start(IContainer container, System.Web.HttpContextBase ctx)
        {            
            System.Web.Mvc.DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
            
            RouteConfig.RegisterRoutes(System.Web.Routing.RouteTable.Routes, container);
            BundleConfig.Register(ctx, container);
        }
    }
}