using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AFGT.Startup))]
namespace AFGT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
