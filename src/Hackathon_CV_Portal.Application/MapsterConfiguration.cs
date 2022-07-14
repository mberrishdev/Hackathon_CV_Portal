using Hackathon_CV_Portal.Application.Implementations.Vacancies.Models;
using Hackathon_CV_Portal.Domain.Vcancies;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Hackathon_CV_Portal.Application
{
    public static class MapsterConfiguration
    {
        public static void RegisterMaps(this IServiceCollection service)
        {
            TypeAdapterConfig<Vacancy, VacancyModel>
            .NewConfig();
        }
    }
}
