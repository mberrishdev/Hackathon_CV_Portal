namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface ICaptchService
    {
        Task<bool> IsCaptchaValid(string token);
    }
}
