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


        //public IActionResult Category()
        //{
        //    LoadUserModel();

        //    var skillModels = _categoryService.GetCV(UserModel.UserId).Result.Cate;

        //    SkillVM skillVm = new SkillVM()
        //    {
        //        SkillModels = skillModels.Select(n => new SkillModel()
        //        {
        //            Id = n.Id,
        //            Title = n.Title,
        //        }).ToList()
        //    };

        //    return View(skillVm);
        //}


        //public IActionResult AddCategory()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddCategory([FromForm] CreateSkillDTO model)
        //{
        //    if (!ModelState.IsValid)
        //        return View();

        //    LoadUserModel();

        //    var command = new CreateSkillCommand()
        //    {
        //        Title = model.Title,
        //        UserId = UserModel.UserId,
        //    };
        //    await _categoryService.AddSkillAsync(command);

        //    return RedirectToAction("Skill");
        //}

        //public async Task<IActionResult> DeleteCategory(int id)
        //{
        //    LoadUserModel();

        //    var cv = await _categoryService.GetCV(UserModel.UserId);

        //    if (cv != null)
        //    {
        //        if (cv.UserId == UserModel.UserId)
        //        {
        //            await _cvService.DeleteSkill(id);
        //        }
        //    }

        //    return RedirectToAction("Skill");
        //}
    }
}
