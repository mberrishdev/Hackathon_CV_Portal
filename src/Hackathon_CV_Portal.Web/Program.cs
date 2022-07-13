using Hackathon_CV_Portal.Persistence.Context;
using Hackathon_CV_Portal.Web.Infrastracture.StartupConfiguration;
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
        options.UseSqlServer(builder.Configuration.GetConnectionString("MovieDBContextConnection"));
    });

    builder.ConfigureService();

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