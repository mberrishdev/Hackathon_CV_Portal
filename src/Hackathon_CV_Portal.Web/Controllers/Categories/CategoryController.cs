using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Domain.Categories.command;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Web.Models.CategoryModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon_CV_Portal.Web.Controllers.Categories
{
    [Authorize]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(SignInManager<ApplicationUser> signInManager, ICategoryService categoryService) : base(signInManager)
        {
            _categoryService = categoryService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetCategories();

            if (result == null)
                return RedirectToAction("NotFound", "Home");

            return View(result);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCategory([FromForm] CreateCategoryDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            LoadUserModel();

            var command = new CreateCategoryCommand()
            {
                Name = model.Name,
            };
            await _categoryService.AddCategory(command);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategory(id);

            return RedirectToAction("Index");
        }
    }
}
