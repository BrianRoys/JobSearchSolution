using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JobSearchSolution.Startup))]
namespace JobSearchSolution
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
