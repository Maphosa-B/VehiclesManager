using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VehiclesManager.Startup))]
namespace VehiclesManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
