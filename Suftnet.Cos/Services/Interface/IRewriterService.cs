namespace Suftnet.Cos.Web
{
    public interface IRewriterService
    {
        System.Web.Mvc.ActionResult RewriteUrl(System.Web.HttpRequestBase Request);
    }
}
