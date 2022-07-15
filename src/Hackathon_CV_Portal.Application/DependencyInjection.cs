using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations;
using Hackathon_CV_Portal.Application.Implementations.Cv;
using Hackathon_CV_Portal.Application.Implementations.Vacancies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Hackathon_CV_Portal.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterMaps();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IVacancyService, VacancyService>();
            services.AddScoped<ICvService, CvService>();

            return services;
        }
    }
}
