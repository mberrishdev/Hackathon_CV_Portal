using Microsoft.AspNetCore.Mvc;

namespace Hackathon_CV_Portal.Web.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
