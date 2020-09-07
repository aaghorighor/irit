namespace Suftnet.Cos.Web
{
    using Suftnet.Cos.Core;
    using StructureMap;
    using Common;

    public class BundleConfig
    {
        public static void Register(System.Web.HttpContextBase ctx, IContainer container)
        {
            var optimizationService = container.GetInstance<IOptimizationService>();
            optimizationService.RegisterBundles(ctx);

            GeneralConfiguration.Configuration.Logger.Log("bundles configured", EventLogSeverity.Information);
        }
    }
}