using Hackathon_CV_Portal.Domain;

namespace Hackathon_CV_Portal.Application.Implementations.Cv.Queries
{
    public class GetCVQuery
    {
        public UserModel UserModel { get; set; }
        public int CvId { get; set; }
    }
}
