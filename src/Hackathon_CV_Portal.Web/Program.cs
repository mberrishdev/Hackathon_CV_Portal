using Hackathon_CV_Portal.Web.Infrastracture.StartupConfiguration;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.ConfigureService();

    var app = builder.Build();

    app.UseDeveloperExceptionPage();
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