using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyIdentitylogin.Startup))]
namespace MyIdentitylogin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
