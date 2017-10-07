using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EssentialNewsMvc.Web.Startup))]
namespace EssentialNewsMvc.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
