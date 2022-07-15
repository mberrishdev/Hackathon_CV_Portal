using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Categories.Vacancies.Models;
using Hackathon_CV_Portal.Data.Abstractions;
using Hackathon_CV_Portal.Domain.Categories;

namespace Hackathon_CV_Portal.Application.Implementations.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<Category> _baseRepository;

        public CategoryService(IBaseRepository<Category> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<List<CategoryVM>> GetCategories()
        {
            var categories = await _baseRepository.GetAllAsyncWithIP(x => x.Vacancies);

            return categories.Select(x => new CategoryVM()
            {
                Id = x.Id,
                Name = x.Name,
                VacancyCount = x.Vacancies.Count
            }).ToList();
        }
    }
}
