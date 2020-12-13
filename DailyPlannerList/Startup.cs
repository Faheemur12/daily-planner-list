using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DailyPlannerList.Startup))]
namespace DailyPlannerList
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
