using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Hackathon_CV_Portal.Application.Implementations.Captchs
{
    public class CaptchService : ICaptchService
    {
        private CaptchSettings _captchSettings { get; }

        public CaptchService(IOptions<CaptchSettings> captchSettings)
        {
            _captchSettings = captchSettings.Value;
        }
        public async Task<bool> IsCaptchaValid(string token)
        {
            try
            {
                var secret = _captchSettings.Secret;
                using (var client = new HttpClient())
                {
                    var dictionary = new Dictionary<string, string>()
                    {
                        { "secret",secret},
                        {"response", token },
                    };
                    var postContent = new FormUrlEncodedContent(dictionary);
                    HttpResponseMessage recaptchaResponse = null;
                    string stringContent = "";
                    using (var http = new HttpClient())
                    {
                        recaptchaResponse = await http.PostAsync("https://www.google.com/recaptcha/api/siteverify", postContent);
                        stringContent = await recaptchaResponse.Content.ReadAsStringAsync();
                    }

                    var googleReCaptchaResponse = JsonConvert.DeserializeObject<ReCaptchaResponse>(stringContent);

                    if (googleReCaptchaResponse.Success && googleReCaptchaResponse.Score > 0.5)
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }

            return false;
        }
    }

    public class ReCaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("score")]
        public float Score { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("challenge_ts")]
        public DateTime ChallengeTs { get; set; }

        [JsonProperty("hostname")]
        public string HostName { get; set; }

        [JsonProperty("error-codes")]
        public string[] ErrorCodes { get; set; }
    }
}
