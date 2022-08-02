using Hackathon_CV_Portal.Domain.AboutUs;
using Hackathon_CV_Portal.Domain.AppliedCCVs;
using Hackathon_CV_Portal.Domain.Categories;
using Hackathon_CV_Portal.Domain.CVs;
using Hackathon_CV_Portal.Domain.Educations;
using Hackathon_CV_Portal.Domain.FavouriteVacancies;
using Hackathon_CV_Portal.Domain.Locations;
using Hackathon_CV_Portal.Domain.Qualifications;
using Hackathon_CV_Portal.Domain.Responsibilities;
using Hackathon_CV_Portal.Domain.Skills;
using Hackathon_CV_Portal.Domain.Users;
using Hackathon_CV_Portal.Domain.Vcancies;
using Hackathon_CV_Portal.Domain.WorkignExperiences;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hackathon_CV_Portal.Persistence.Context
{
    public class CvPortalDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public CvPortalDbContext(DbContextOptions<CvPortalDbContext> options) : base(options)
        {
        }

        public DbSet<About> About { get; set; }
        public DbSet<CurriculumVitae> CVs { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<FavouriteVacancy> FavouriteVacancies { get; set; }
        public DbSet<AppliedCurriculumVitae> AppliedCurriculumVitaes { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<WorkingExperience> WorkignExperiences { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Responsibility> Responsibilities { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CvPortalDbContext).Assembly);

            modelBuilder.Entity<ApplicationUserRole>().HasOne(item => item.Role).WithMany(item => item.UserRoles).HasForeignKey(item => item.RoleId);
            modelBuilder.Entity<ApplicationUserRole>().HasOne(item => item.User).WithMany(item => item.UserRoles).HasForeignKey(item => item.UserId);


            //modelBuilder.Entity<AppliedCurriculumVitae>().HasOne(item => item.Vacancy).WithMany(item => item.AppliedCurriculumVitaes).HasForeignKey(item => item.VacancyId);
            //modelBuilder.Entity<AppliedCurriculumVitae>().HasOne(item => item.CurriculumVitae).WithMany(item => item.AppliedCurriculumVitaes).HasForeignKey(item => item.CVId);


            //modelBuilder.Entity<FavouriteVacancie>().HasOne(item => item.Vacancie).WithMany(item => item.FavouriteVacancies).HasForeignKey(item => item.VacansyId);
            //modelBuilder.Entity<FavouriteVacancie>().HasOne(item => item.User).WithMany(item => item.FavouriteVacancies).HasForeignKey(item => item.UserId);
            //modelBuilder.Entity<FavouriteVacancie>()
            //             .HasKey(x => x.Id);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.CVs)
                .WithOne(u => u.User);

            modelBuilder.Entity<Category>()
                .HasMany(V => V.Vacancies)
                .WithOne(c => c.Category)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Location>()
                .HasMany(V => V.Vacancies)
                .WithOne(l => l.Location)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(v => v.Vacancies)
                .WithOne(u => u.User);

            modelBuilder.Entity<CurriculumVitae>()
                .HasMany(e => e.Educations)
                .WithOne(c => c.CV);

            modelBuilder.Entity<CurriculumVitae>()
                .HasMany(s => s.Skills)
                .WithOne(c => c.CV);

            modelBuilder.Entity<CurriculumVitae>()
                .HasMany(w => w.WorkingExperience)
                .WithOne(c => c.CV);

            modelBuilder.Entity<Education>()
                .HasOne(c => c.CV)
                .WithMany(e => e.Educations);

            modelBuilder.Entity<WorkingExperience>()
               .HasOne(c => c.CV)
               .WithMany(w => w.WorkingExperience);

            modelBuilder.Entity<Skill>()
               .HasOne(c => c.CV)
               .WithMany(s => s.Skills);

            modelBuilder.Entity<Responsibility>()
               .HasOne(v => v.Vacancy)
               .WithMany(r => r.Responsibilities);

            modelBuilder.Entity<Qualification>()
               .HasOne(v => v.Vacancy)
               .WithMany(q => q.Qualifications);
        }
    }
}
