using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StarInsurance.MVC.Startup))]
namespace StarInsurance.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
