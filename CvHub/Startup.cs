using Data;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CvHub.Startup))]
namespace CvHub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.CreatePerOwinContext(() => new ApplicationDbContext());
        }
    }
}
