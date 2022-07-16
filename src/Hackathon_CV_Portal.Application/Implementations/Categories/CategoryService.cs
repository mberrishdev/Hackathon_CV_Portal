using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Categories.Vacancies.Models;
using Hackathon_CV_Portal.Data.Abstractions;
using Hackathon_CV_Portal.Domain.Categories;
using Hackathon_CV_Portal.Domain.Categories.command;
using Hackathon_CV_Portal.Persistence.Context;

namespace Hackathon_CV_Portal.Application.Implementations.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<Category> _baseRepository;
        private readonly CvPortalDbContext _context;

        public CategoryService(IBaseRepository<Category> baseRepository, CvPortalDbContext context)
        {
            _baseRepository = baseRepository;
            _context = context;
        }

        public async Task AddCategory(CreateCategoryCommand command)
        {
            Category category = new Category()
            {
                Name = command.Name,
            };
            
            _context.Categories.Add(category);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id); 
            var vacancies = _context.Vacancies.Where(x => x.CategoryId == id);

            if(category != null)
            {
                _context.RemoveRange(vacancies);
                _context.Remove(category);
            }

            await _context.SaveChangesAsync();
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
