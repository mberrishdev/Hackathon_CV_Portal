using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations;
using Hackathon_CV_Portal.Application.Implementations.AppliedCurriculumVitaes;
using Hackathon_CV_Portal.Application.Implementations.Categories;
using Hackathon_CV_Portal.Application.Implementations.Cv;
using Hackathon_CV_Portal.Application.Implementations.FavouriteVacancies;
using Hackathon_CV_Portal.Application.Implementations.Users;
using Hackathon_CV_Portal.Application.Implementations.Vacancies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hackathon_CV_Portal.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterMaps();

            services.Configure<IdentityOptions>(opts =>
            {
                opts.Lockout.AllowedForNewUsers = true;
                opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                opts.Lockout.MaxFailedAccessAttempts = 3;
            });

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IFavouriteVacancyService, FavouriteVacancyService>();
            services.AddScoped<IVacancyService, VacancyService>();
            services.AddScoped<ICvService, CvService>();
            services.AddScoped<IAppliedCurriculumVitaeService, AppliedCurriculumVitaeService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
