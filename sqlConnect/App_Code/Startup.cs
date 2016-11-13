using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sqlConnect.Startup))]
namespace sqlConnect
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
