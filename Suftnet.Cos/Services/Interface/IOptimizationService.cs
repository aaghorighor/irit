namespace Suftnet.Cos.Web
{
    using System.Web;

    public interface IOptimizationService
    {
        void GenerateMetasInformations(HttpContextBase context, object model);
        void RegisterBundles(HttpContextBase ctx);
    }
}