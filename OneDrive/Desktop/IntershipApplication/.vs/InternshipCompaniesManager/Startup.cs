using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InternshipCompaniesManager.Startup))]
namespace InternshipCompaniesManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
