using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hackathon_CV_Portal.Application.Implementations.AboutUs.Models;
using Hackathon_CV_Portal.Domain.AboutUs.Commands;

namespace Hackathon_CV_Portal.Application.Abstractions
{
    public interface IAboutService
    {
        Task<Hackathon_CV_Portal.Domain.AboutUs.About> GetAboutUs();
        Task UpdateAboutUs(CreateAboutUsCommand command);
        Task CreateAboutUs(CreateAboutUsCommand command);
    }
}
