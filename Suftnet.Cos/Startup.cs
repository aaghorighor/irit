using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Suftnet.Cos.Web.Startup))]
namespace Suftnet.Cos.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}