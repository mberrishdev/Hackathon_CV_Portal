using Hackathon_CV_Portal.Application;
using Hackathon_CV_Portal.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

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

            //services.AddAntiforgery(options =>
            //{
            //    options.FormFieldName = "AntiforgeryFieldname";
            //    options.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
            //    options.SuppressXFrameOptionsHeader = false;
            //});


            // Add services to the container.
            services.AddControllersWithViews(options =>
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

            services.AddRepository();
            services.AddApplication(configuration);

            //CvPortalSeed.Initialize(services.BuildServiceProvider());

            //services.AddIdentity<IdentityUser, IdentityRole>()
            //    .AddEntityFrameworkStores<CvPortalDbContext>();

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<CvPortalDbContext>();


            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("AspManager", policy =>
                {
                    policy.RequireRole("Test");
                });
            });

            return builder;
        }
    }
}
