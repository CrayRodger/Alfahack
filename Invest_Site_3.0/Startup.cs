using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Invest_Site_3._0.Startup))]
namespace Invest_Site_3._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
