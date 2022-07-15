using Hackathon_CV_Portal.Application.Categories.Vacancies.Models;

namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface ICategoryService
    {
        Task<List<CategoryVM>> GetCategories();
    }
}
