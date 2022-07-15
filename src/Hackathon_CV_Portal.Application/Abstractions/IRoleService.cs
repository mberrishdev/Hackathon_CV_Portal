namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface IRoleService
    {
        public Task<List<string>> GetUserRoleAsync(int userId);
    }
}
