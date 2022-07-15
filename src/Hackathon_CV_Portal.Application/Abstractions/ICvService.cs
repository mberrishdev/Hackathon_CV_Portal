using Hackathon_CV_Portal.Application.Implementations.Cv.Models;
using Hackathon_CV_Portal.Domain.CVs.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface ICvService
    {
        Task<CvVM> GetCV(int userId); 
        Task CreateCv(CreateCvCommand command); 
    }
}
