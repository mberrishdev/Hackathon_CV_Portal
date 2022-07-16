using Hackathon_CV_Portal.Domain.Categories;
using Hackathon_CV_Portal.Domain.CVs;
using Hackathon_CV_Portal.Domain.Educations;
using Hackathon_CV_Portal.Domain.Enums;
using Hackathon_CV_Portal.Domain.Skills;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Domain.Vcancies;
using Hackathon_CV_Portal.Domain.WorkignExperiences;
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

            //Migrate(database);
            Seed(database);
        }

        private static void Seed(CvPortalDbContext context)
        {
            var seeded = false;

            SeedRoles(context, ref seeded);
            SeedUsers(context, ref seeded);
            SeedCategories(context, ref seeded);
            SeedCv(context, ref seeded);
            SeedVacancies(context, ref seeded);

            if (seeded)
                context.SaveChanges();
        }

        private static void SeedCv(CvPortalDbContext context, ref bool seeded)
        {
            var curriculumVitaes = new List<CurriculumVitae>()
            {
               new CurriculumVitae()
               {
                   FirstName = "Rati",
                   LastName = "Tkhilaishvili",
                   BirtDate = new DateTime((DateTime.Now - new TimeSpan(10,10,10,10)).Ticks),
                   PhoneNumber = "568280025",
                   Email = "ratitkhilaishvili@gmail.com",
                   Address = "Nutsubidze str 139",
                   AboutMe = "I am web developer",
                   Educations = new List<Education>()
                   {
                       new Education() {
                           StartDate = DateTime.Now,
                           EndDate = DateTime.Now + new TimeSpan(10,10,10),
                           Name = "Computer Science",
                           Description = "I was Cs Student",
                           University = "Caucasus university",
                           City = "Tbilisi"
                       },

                       new Education() {
                           StartDate = DateTime.Now,
                           EndDate = DateTime.Now + new TimeSpan(10,10,10),
                           Name = "Computer Science",
                           Description = "I was Cs Student",
                           University = "Caucasus university",
                           City = "Tbilisi"
                       }
                   },
                   WorkingExperience = new List<WorkingExperience>()
                   {
                       new WorkingExperience() {
                           StartDate = DateTime.Now,
                           EndDate = DateTime.Now + new TimeSpan(10,10,10),
                           Name = "Computer Science",
                           Description = "I was Cs Student",
                           Company = "Smartsoft",
                           City = "Tbilisi"
                       },

                       new WorkingExperience() {
                           StartDate = DateTime.Now,
                           EndDate = DateTime.Now + new TimeSpan(10,10,10),
                           Name = "Computer Science",
                           Description = "I was Cs Student",
                           Company = "Smartsoft",
                           City = "Tbilisi"
                       },

                       new WorkingExperience() {
                           StartDate = DateTime.Now,
                           EndDate = DateTime.Now + new TimeSpan(10,10,10),
                           Name = "Computer Science",
                           Description = "I was Cs Student",
                           Company = "Smartsoft",
                           City = "Tbilisi"
                       },
                   },
                   Skills = new List<Skill>()
                   {
                       new Skill ()
                       {
                           Title = "C#"
                       },

                       new Skill ()
                       {
                           Title = "C#"
                       },

                       new Skill ()
                       {
                           Title = "C#"
                       }
                   },
                   Image = "",
                   UserId = 5,
               }
            };

            foreach (var cv in curriculumVitaes)
            {
                if (!context.CVs.Any(item => item.Id == cv.Id))
                {
                    context.CVs.Add(cv);
                    seeded = true;
                }
            }
        }

        private static void SeedCategories(CvPortalDbContext context, ref bool seeded)
        {
            var categories = new List<Category>()
            {
                new Category()
                {
                    Name = "IT",
                },
                new Category()
                {
                    Name = "Marketing",
                },
            };

            foreach (var category in categories)
            {
                if (!context.Categories.Any(item => item.Id == category.Id))
                {
                    context.Categories.Add(category);
                    seeded = true;
                }
            }
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
                new ApplicationUser()
                {
                    UserName = "rati_t",
                    PasswordHash = "AQAAAAEAACcQAAAAEAShB0tfptCVEFl0XUy4moFAinxQutVQd0qoDNelMheh6zhzMBhJUx/oBJn9wxBklw=="
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

            for (int i = 0; i < 25; i++)
            {
                var vac = new Vacancy()
                {
                    Title = $"Vac{i}",
                    SalaryRange = $"{100 + i}-{200 + i} $",
                    CompanyName = $"Facebook {1}",
                    Location = $"Tbilisi, Georgia {1}",
                    PublishDate = DateTime.Now,
                    DeadLine = DateTime.Now,
                    Description = "bla bla",
                    Type = VacancyType.PartTime,
                    UserId = 1,
                    CategoryId = 1,
                    Responsibility = "** მაღაზიაში არსებული სტანდარტის მიხედვით მომხმარებლისათვის კვალიფიციური მომსახურების და კონსულტაციის გაწევა" +
                                    "* *პროდუქციის მოწესრიგება," +
                                     "მაღაზიის ვიზუალზე ზრუნვა" +
                                    "* *მომხმარებლისათვის პროდუქციის ეფექტური შეთავაზება," +
                                     " გაყიდვების ზრდაზე ზრუნვა" +
                                    "* *მაღაზიის ყოველდღიური მუშაობის პროცესში ჩართულობა.,",
                    Qualifications = "** ინტერესი მოდის ინდუსტრიის მიმართ, მსოფლიო მოდის ტენდენციებისა და ლუქს კლასის ბრენდების კარგი ცოდნა, სტილისა და ფერის შეგრძნება, კრეატიულობა და კარგი ხედვა" +
                                    "** ასაკი : 25+ (ასაკობრივი ზედა ზღვარის, სქესისა და სხვა შეზღუდვის გარეშე)" +
                                    "** მსგავს პოზიციაზე მინიმუმ 2 წლიანი სამუშაო გამოცდილება;" +
                                    "** ქართული ენის სრულყოფილად ცოდნა. ინგლისური და სხვა უცხო ენები- კარგი, თავისუფალი საკომუნიკაციო დონე"
                };

                context.Vacancies.Add(vac);
            }

            for (int i = 0; i < 25; i++)
            {
                var vac = new Vacancy()
                {
                    Title = $"Vac{i}",
                    SalaryRange = $"{100 + i}-{200 + i} $",
                    CompanyName = $"Facebook {1}",
                    Location = $"Tbilisi, Georgia {1}",
                    PublishDate = DateTime.Now,
                    DeadLine = DateTime.Now,
                    Description = "bla bla",
                    Type = VacancyType.FullTime,
                    UserId = 1,
                    CategoryId = 1,
                    Responsibility = "",
                    Qualifications = "",
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
