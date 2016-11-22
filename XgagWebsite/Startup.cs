using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XgagWebsite.Startup))]
namespace XgagWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
