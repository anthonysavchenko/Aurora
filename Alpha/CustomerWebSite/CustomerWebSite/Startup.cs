using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CustomerWebSite.Startup))]
namespace CustomerWebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
