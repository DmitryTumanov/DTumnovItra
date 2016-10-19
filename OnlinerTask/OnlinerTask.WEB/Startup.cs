using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(OnlinerTask.WEB.Startup))]

namespace OnlinerTask.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
