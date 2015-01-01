using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WoofQAWebsite.Startup))]
namespace WoofQAWebsite
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app) 
        {
            ConfigureAuth(app);
        }
    }
}
