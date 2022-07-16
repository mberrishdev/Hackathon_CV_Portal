using Hackathon_CV_Portal.Application.Categories.Vacancies.Models;
using Hackathon_CV_Portal.Domain.Categories.command;

namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>> GetCategories();
        Task AddCategory(CreateCategoryCommand command);
        Task DeleteCategory(int id);
    }
}
