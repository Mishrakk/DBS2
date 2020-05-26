using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DVDRentalStore.Startup))]
namespace DVDRentalStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
