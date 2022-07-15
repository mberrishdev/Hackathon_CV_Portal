using Hackathon_CV_Portal.Domain.CVs;
using Hackathon_CV_Portal.Domain.Users.Commands;
using Hackathon_CV_Portal.Domain.Vcancies;
using Microsoft.AspNetCore.Identity;

namespace Hackathon_CV_Portal.Domain.Users
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ICollection<CurriculumVitae> CVs { get; set; }
        public ICollection<Vacancy> Vacancies { get; set; }

        public DateTime CreateDate { get; private set; }

        public ICollection<ApplicationUserRole> UserRoles { get; set; }
        // public ICollection<FavouriteVacancie> FavouriteVacancies { get; set; }

        public ApplicationUser() { }

        public ApplicationUser(CreateAppilicationUserCommand command)
        {
            UserName = command.UserName;
            Email = command.Email;
            CreateDate = DateTime.Now;
        }
    }
}
