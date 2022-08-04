using Hackathon_CV_Portal.Application.Locations.Vacancies.Models;
using Hackathon_CV_Portal.Domain.Locations.Commands;

namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface ILocationService
    {
        Task<List<LocationVM>> GetLocations();
        Task<int> AddLocation(CreateLocationCommand command);
        Task DeleteLocation(int id);
    }
}
