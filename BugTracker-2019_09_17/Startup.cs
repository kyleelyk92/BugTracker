using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BugTracker_2019_09_17.Startup))]
namespace BugTracker_2019_09_17
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
