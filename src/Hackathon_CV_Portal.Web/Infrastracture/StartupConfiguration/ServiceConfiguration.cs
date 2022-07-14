using Hackathon_CV_Portal.Application;
using Hackathon_CV_Portal.Data;
using Hackathon_CV_Portal.Persistence.Seed;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Hackathon_CV_Portal.Web.Infrastracture.StartupConfiguration
{
    public static class ServiceConfiguration
    {
        public static WebApplicationBuilder ConfigureService(this WebApplicationBuilder builder)
        {
            IServiceCollection services = builder.Services;
            IConfiguration configuration = builder.Configuration;

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(option =>
            {
                option.LoginPath = new PathString("/Account/Login");
                option.AccessDeniedPath = new PathString("/Auth/AccessDenied");
                option.Events = new CookieAuthenticationEvents
                {
                    OnSignedIn = async ctx =>
                    {
                        ctx.HttpContext.User = ctx.Principal;
                    }
                };
            });

            // Add services to the container.
            services.AddControllersWithViews();
            services.AddRepository();
            services.AddApplication(configuration);

            CvPortalSeed.Initialize(services.BuildServiceProvider());

            //services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<CvPortalDbContext>();

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<CvPortalDbContext>();

            return builder;
        }
    }
}
