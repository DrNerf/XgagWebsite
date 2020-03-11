using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System.Net;
using System.Web.Http;

[assembly: OwinStartupAttribute(typeof(XgagWebsite.Startup))]
namespace XgagWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }
    }
}
