using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Settings;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Web.Models.Contact;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Hackathon_CV_Portal.Web.Controllers.Contact
{
    public class ContactController : BaseController
    {
        private EmailSettings _emailSettings { get; }
        private readonly ICaptchService _captchService;

        private readonly IEmailSender _emailSender;
        public ContactController(SignInManager<ApplicationUser> signInManager, IOptions<EmailSettings> emailSettings, IEmailSender emailSender, ICaptchService captchService) : base(signInManager)
        {
            _emailSender = emailSender;
            _emailSettings = emailSettings.Value;
            _captchService = captchService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactUs([FromForm] ContactUsDto model)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", model);

            var isCaptchaValid = await _captchService.IsCaptchaValid(model.GoogleCaptchaToken);
            if (!isCaptchaValid)
                RedirectToAction("Error", "Home");

            var body = "Name: " + model.Name + "<br/><br />Email: " + model.Email + "<br />" + model.Message;
            await _emailSender.SendEmailAsync(_emailSettings.ContactUsMailAddress, "Contact Us - Email", body);

            return RedirectToAction("Success", "Home");
        }
    }
}
