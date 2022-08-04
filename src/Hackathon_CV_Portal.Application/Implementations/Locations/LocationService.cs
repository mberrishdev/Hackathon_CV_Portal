using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Locations.Vacancies.Models;
using Hackathon_CV_Portal.Data.Abstractions;
using Hackathon_CV_Portal.Domain.Locations;
using Hackathon_CV_Portal.Domain.Locations.Commands;

namespace Hackathon_CV_Portal.Application.Implementations.Locations
{
    public class LocationService : ILocationService
    {
        private readonly IBaseRepository<Location> _baseRepository;

        public LocationService(IBaseRepository<Location> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<int> AddLocation(CreateLocationCommand command)
        {
            var location = new Location(command);

            return await _baseRepository.CreateAsync(location);
        }

        public async Task DeleteLocation(int id)
        {
            var location = await _baseRepository.GetAsync(predicate: x => x.Id == id);
            if (location != null)
            {
                await _baseRepository.RemoveAsync(location);
            }
        }

        public async Task<List<LocationVM>> GetLocations()
        {
            var locations = await _baseRepository.GetAllAsyncWithIP(x => x.Vacancies);

            return locations.Select(x => new LocationVM()
            {
                Id = x.Id,
                Country = x.Country,
                City = x.City
            }).Reverse().ToList();
        }
    }
}
