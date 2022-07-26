using Hackathon_CV_Portal.Web.Controllers.Accounts;
using Microsoft.AspNetCore.Mvc;

namespace Hackathon_CV_Portal.Web.Infrastracture.Extensions
{
    public static class UrlHelperExtensions
    {

        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, int userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: nameof(AccountController.ResetPassword),
                controller: "Account",
                values: new { userId, code },
                protocol: scheme);
        }
    }
}
