using Hackathon_CV_Portal.Application.Implementations.Cv.Models;
using Hackathon_CV_Portal.Application.Implementations.Cv.Queries;
using Hackathon_CV_Portal.Domain.CVs.Commands;

namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface ICvService
    {
        Task<CvVM> GetCV(GetCVQuery query);
        Task CreateCv(CreateCvCommand command);
        Task<CvModel> GetCVById(int cvId);
    }
}
