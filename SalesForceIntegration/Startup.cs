using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SalesForceIntegration.Startup))]
namespace SalesForceIntegration
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
