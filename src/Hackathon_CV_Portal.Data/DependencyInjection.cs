using Hackathon_CV_Portal.Data.Abstractions;
using Hackathon_CV_Portal.Data.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Hackathon_CV_Portal.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            return services;
        }
    }
}
