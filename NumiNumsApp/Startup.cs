using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NumiNumsApp.Startup))]
namespace NumiNumsApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
