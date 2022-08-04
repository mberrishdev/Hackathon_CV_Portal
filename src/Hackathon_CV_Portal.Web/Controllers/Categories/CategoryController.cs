using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Domain.Categories.command;
using Hackathon_CV_Portal.Domain.Users;
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCategory(string categoryName)
        {
            var command = new CreateCategoryCommand()
            {
                Name = categoryName,
            };

            var id = await _categoryService.AddCategory(command);

            return Ok(id);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategory(id);

            return RedirectToAction("Index");
        }
    }
}
