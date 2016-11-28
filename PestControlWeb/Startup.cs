using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PestControlWeb.Startup))]
namespace PestControlWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
