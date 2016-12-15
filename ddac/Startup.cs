using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ddac.Startup))]
namespace ddac
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
