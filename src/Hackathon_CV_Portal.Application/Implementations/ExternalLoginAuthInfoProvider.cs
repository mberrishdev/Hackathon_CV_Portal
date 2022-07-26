using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceStack.Auth;
using ServiceStack.Text;
using System.Security.Claims;

namespace Hackathon_CV_Portal.Application.Implementations
{
    public class ExternalLoginAuthInfoProvider : IExternalLoginAuthInfoProvider
    {
        private readonly IConfiguration configuration;
        private readonly IAuthHttpGateway authGateway;

        public ExternalLoginAuthInfoProvider(IConfiguration configuration, IAuthHttpGateway authHttpGateway = null)
        {
            this.configuration = configuration;
            this.authGateway = authHttpGateway ?? new AuthHttpGateway();
        }

        public void PopulateUser(ExternalLoginInfo info, ApplicationUser user)
        {
            var accessToken = info.AuthenticationTokens.FirstOrDefault(x => x.Name == "access_token");
            var accessTokenSecret = info.AuthenticationTokens.FirstOrDefault(x => x.Name == "access_token_secret");

            if (info.LoginProvider == "Facebook")
            {
                user.FacebookUserId = info.Principal?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                user.UserName = info.Principal?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                user.FirstName = info.Principal?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
                user.LastName = info.Principal?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value;

                if (accessToken != null)
                {
                    var facebookInfo = JsonObject.Parse(authGateway.DownloadFacebookUserInfo(accessToken.Value, "picture"));
                    var picture = facebookInfo.Object("picture");
                    var data = picture?.Object("data");
                    if (data != null)
                    {
                        if (data.TryGetValue("url", out var profileUrl))
                        {
                            user.ProfileUrl = profileUrl.SanitizeOAuthUrl();
                        }
                    }
                }
            }
            else if (info.LoginProvider == "Google")
            {
                user.UserName = info.Principal?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
                user.GoogleUserId = info.Principal?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                user.DisplayName = info.Principal?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
                user.FirstName = info.Principal?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
                user.LastName = info.Principal?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value;
                user.GoogleProfilePageUrl = info.Principal?.Claims.FirstOrDefault(x => x.Type == "urn:google:profile")?.Value;

                if (accessToken != null)
                {
                    var googleInfo = JsonObject.Parse(authGateway.DownloadGoogleUserInfo(accessToken.Value));
                    user.ProfileUrl = googleInfo.Get("picture").SanitizeOAuthUrl();
                }
            }
        }
    }
}
