using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JQueryDataTableWithAspNetMVC.Startup))]
namespace JQueryDataTableWithAspNetMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
