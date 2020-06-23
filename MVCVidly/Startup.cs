using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCVidly.Startup))]
namespace MVCVidly
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
