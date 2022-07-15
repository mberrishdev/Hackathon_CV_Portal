using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
    }
}
