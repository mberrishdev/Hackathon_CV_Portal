using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Web.Models.CategoryModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Hackathon_CV_Portal.Domain.Categories.command;

namespace Hackathon_CV_Portal.Web.Controllers.Categories
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(SignInManager<ApplicationUser> signInManager, ICategoryService categoryService) : base(signInManager)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetCategories();

            if (result == null)
                return RedirectToAction("NotFound", "Home");

            return View(result);
        }

        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromForm] CreateCategoryDto model)
        {
            if (!ModelState.IsValid)
                return View();

            LoadUserModel();

            var command = new CreateCategoryCommand()
            {
                Name = model.Name,
            };
            await _categoryService.AddCategory(command);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategory(id);

            return RedirectToAction("Index");
        }
    }
}
