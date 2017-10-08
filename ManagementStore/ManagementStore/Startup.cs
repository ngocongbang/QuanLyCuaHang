using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ManagementStore.Startup))]
namespace ManagementStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
