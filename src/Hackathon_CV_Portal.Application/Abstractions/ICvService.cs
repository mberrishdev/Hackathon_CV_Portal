using Hackathon_CV_Portal.Application.Implementations.Cv.Models;
using Hackathon_CV_Portal.Domain.CVs.Commands;
using Hackathon_CV_Portal.Domain.Educations.Commands;
using Hackathon_CV_Portal.Domain.Skills.Commands;
using Hackathon_CV_Portal.Domain.WorkignExperiences.Commands;
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

        Task AddSkillAsync(CreateSkillCommand command);
        Task AddWorkingExperienceAsync(CreateWorkingExperienceCommand command);
        Task AddEducationAsync(CreateEducationCommand command);
        Task DeleteWorkingExperience(int id);
        Task DeleteSkill(int id);
        Task DeleteEducation(int id);
    }
}
