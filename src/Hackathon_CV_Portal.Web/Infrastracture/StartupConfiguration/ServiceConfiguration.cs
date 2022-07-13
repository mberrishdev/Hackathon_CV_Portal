using Hackathon_CV_Portal.Application;

namespace Hackathon_CV_Portal.Web.Infrastracture.StartupConfiguration
{
    public static class ServiceConfiguration
    {
        public static WebApplicationBuilder ConfigureService(this WebApplicationBuilder builder)
        {
            IServiceCollection services = builder.Services;
            IConfiguration configuration = builder.Configuration;


            // Add services to the container.
            services.AddControllersWithViews();

            services.AddApplication(configuration);

            return builder;
        }
    }
}
