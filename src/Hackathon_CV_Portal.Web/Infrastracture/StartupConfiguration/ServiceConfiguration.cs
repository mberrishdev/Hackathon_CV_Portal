using Hackathon_CV_Portal.Application;
using Hackathon_CV_Portal.Data;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Persistence.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hackathon_CV_Portal.Web.Infrastracture.StartupConfiguration
{
    public static class ServiceConfiguration
    {
        public static WebApplicationBuilder ConfigureService(this WebApplicationBuilder builder)
        {
            IServiceCollection services = builder.Services;
            IConfiguration configuration = builder.Configuration;

            builder.Services.AddDbContext<CvPortalDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("HackathonCvPortalContextConnection"));
            });

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddRepository();
            services.AddApplication(configuration);

            //CvPortalSeed.Initialize(services.BuildServiceProvider());

            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
                   .AddEntityFrameworkStores<CvPortalDbContext>()
                   .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider); ;

            services.AddAuthorization(opts =>
            {
                opts.AddPolicy("AspManager", policy =>
                {
                    policy.RequireRole("Test");
                });
            });

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
                 })
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = configuration["Authentication:Google:ClientSecret"];
                })
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = configuration["Authentication:Facebook:AppSecret"];
                })
                ;

            builder.Services.AddRazorPages();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });
            return builder;
        }
    }
}
