using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project_TL.Startup))]
namespace Project_TL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
