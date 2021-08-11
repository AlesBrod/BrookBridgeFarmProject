using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BrookBridgeFarmProject.Startup))]
namespace BrookBridgeFarmProject
{
    public partial class Startup
    {

        //this run whenever app first start
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

        }



    }
}
