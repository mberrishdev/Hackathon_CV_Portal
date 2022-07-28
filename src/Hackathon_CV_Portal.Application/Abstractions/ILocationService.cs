using Hackathon_CV_Portal.Application.Locations.Vacancies.Models;

namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface ILocationService
    {
        Task<List<LocationVM>> GetLocations();
    }
}
