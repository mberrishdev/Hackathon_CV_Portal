using Hackathon_CV_Portal.Domain.Enums;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Domain.Vcancies;
using Hackathon_CV_Portal.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hackathon_CV_Portal.Persistence.Seed
{
    public class CvPortalSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using var scope = serviceProvider.CreateScope();
            var database = scope.ServiceProvider.GetRequiredService<CvPortalDbContext>();

            Migrate(database);
            Seed(database);
        }

        private static void Seed(CvPortalDbContext context)
        {
            var seeded = false;

            SeedRoles(context, ref seeded);
            SeedUsers(context, ref seeded);
            // SeedVacancies(context, ref seeded);

            if (seeded)
                context.SaveChanges();
        }

        private static void SeedRoles(CvPortalDbContext context, ref bool seeded)
        {
            var roles = new List<ApplicationRole>()
            {
                new ApplicationRole()
                {
                    Name = UserRole.User.ToString(),
                    NormalizedName  = UserRole.User.ToString(),
                },
                new ApplicationRole()
                {
                    Name =  UserRole.Company.ToString(),
                    NormalizedName  = UserRole.Company.ToString(),
                },
            };

            foreach (var role in roles)
            {
                if (!context.Roles.Any(item => item.Name == role.Name))
                {
                    context.Roles.Add(role);
                    seeded = true;
                }
            }
        }
        private static void SeedUsers(CvPortalDbContext context, ref bool seeded)
        {
            var users = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    UserName = "mberrish",
                    PasswordHash = "AQAAAAEAACcQAAAAENkqQwx7c2/It1LujA4mLx4wb/oW4ltrNPBEC2cgXPg3Uf6NqHBqbLY37KBIo+1IGg=="
                },

            };

            foreach (var user in users)
            {
                if (!context.Users.Any(item => item.UserName == user.UserName))
                {
                    context.Users.Add(user);
                    seeded = true;
                }
            }
        }

        private static void SeedVacancies(CvPortalDbContext context, ref bool seeded)
        {

            for (int i = 0; i < 50; i++)
            {
                var vac = new Vacancy()
                {
                    Title = $"Vac{i}",
                    Salary = 100 + i,
                    Currency = "USD",
                    PublishDate = DateTime.Now,
                    DeadLine = DateTime.Now,
                    Description = "bla bla",
                    UserId = 1,
                    CategoryId = 1,
                };

                context.Vacancies.Add(vac);
                seeded = true;
            }
        }

        private static void Migrate(CvPortalDbContext context)
        {
            context.Database.Migrate();
        }
    }
}
