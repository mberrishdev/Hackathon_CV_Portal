using Hackathon_CV_Portal.Web.Infrastracture.StartupConfiguration;
using Hackathon_CV_Portal.Web.Worker;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.ConfigureService();

    builder.Services.AddHostedService<VacancyCleanerWorker>();

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