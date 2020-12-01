using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GuildCarsMax.Startup))]
namespace GuildCarsMax
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
