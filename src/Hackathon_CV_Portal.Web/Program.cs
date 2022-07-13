using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Persistence.Context;
using Hackathon_CV_Portal.Web.Infrastracture.StartupConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

//Log.Logger = new LoggerConfiguration()
//    .WriteTo.Console()
//    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddDbContext<CvPortalDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("HackathonCvPortalContextConnection"));
    });

    builder.ConfigureService();

    builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                    .AddEntityFrameworkStores<CvPortalDbContext>();

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

    var app = builder.Build();

    app.ConfigureMiddleware();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpecredly");
}
finally
{
    Log.CloseAndFlush();
}