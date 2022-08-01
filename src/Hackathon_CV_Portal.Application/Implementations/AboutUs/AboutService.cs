using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Implementations.AboutUs.Models;
using Hackathon_CV_Portal.Data.Abstractions;
using Hackathon_CV_Portal.Domain.AboutUs.Commands;

namespace Hackathon_CV_Portal.Application.Implementations.AboutUs
{
    public class AboutService : IAboutService
    {
        private readonly IBaseRepository<Hackathon_CV_Portal.Domain.AboutUs.About> _baseRepository;

        public AboutService(IBaseRepository<Hackathon_CV_Portal.Domain.AboutUs.About> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task CreateAboutUs(CreateAboutUsCommand command)
        {
            Hackathon_CV_Portal.Domain.AboutUs.About about = new Hackathon_CV_Portal.Domain.AboutUs.About()
            {
                Content = command.Content,
            };

            await _baseRepository.CreateAsync(about);
        }

        public async Task<Hackathon_CV_Portal.Domain.AboutUs.About> GetAboutUs()
        {
            return await _baseRepository.GetAsync(null, x => true);
        }

        public async Task UpdateAboutUs(CreateAboutUsCommand command)
        {
            var about = await GetAboutUs();

            if (about != null)
            {
                about.Content = command.Content;
            }

            await _baseRepository.UpdateAsync(about);
        }
    }
}
