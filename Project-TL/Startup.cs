using Microsoft.Owin;
using Owin;
using Project_TL.Models.DAL;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(Project_TL.Startup))]
namespace Project_TL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Context x = new Context();
            x.Firms.ToList();
           
        }
    }
}
