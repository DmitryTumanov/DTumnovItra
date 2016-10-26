using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("DataStartup",typeof(OnlinerTask.Data.DataStartUp))]

namespace OnlinerTask.Data
{
    public class DataStartUp
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
