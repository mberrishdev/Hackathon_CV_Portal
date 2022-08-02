using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Categories.Vacancies.Models;
using Hackathon_CV_Portal.Data.Abstractions;
using Hackathon_CV_Portal.Domain.Categories;
using Hackathon_CV_Portal.Domain.Categories.command;

namespace Hackathon_CV_Portal.Application.Implementations.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<Category> _baseRepository;

        public CategoryService(IBaseRepository<Category> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task AddCategory(CreateCategoryCommand command)
        {
            Category category = new()
            {
                Name = command.Name,
            };

            await _baseRepository.CreateAsync(category);
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _baseRepository.GetAsync(predicate: x => x.Id == id);
            if (category != null)
            {
                await _baseRepository.RemoveAsync(category);
            }
        }

        public async Task<List<CategoryVM>> GetCategories()
        {
            var categories = await _baseRepository.GetAllAsyncWithIP(x => x.Vacancies);

            return categories.Select(x => new CategoryVM()
            {
                Id = x.Id,
                Name = x.Name,
                VacancyCount = x.Vacancies.Count
            }).Reverse().ToList();
        }
    }
}
