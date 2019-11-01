using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MittInternPortal.Startup))]
namespace MittInternPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
