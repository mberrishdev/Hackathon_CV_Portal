using Hackathon_CV_Portal.Application.Abstractions;

namespace Hackathon_CV_Portal.Web.Worker
{
    public sealed class VacancyCleanerWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvicer;

        public VacancyCleanerWorker(IServiceProvider serviceProvicer)
        {
            _serviceProvicer = serviceProvicer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var serviceScope = _serviceProvicer.CreateScope())
                {
                    var vacancyService = serviceScope.ServiceProvider.GetService<IVacancyService>();
                    await vacancyService.CleanVacancies();
                }

                await Task.Delay(3600000, stoppingToken);
            }
        }
    }
}


