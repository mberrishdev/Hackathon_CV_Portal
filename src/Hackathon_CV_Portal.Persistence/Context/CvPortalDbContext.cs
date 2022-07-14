using Hackathon_CV_Portal.Domain.CVs;
using Hackathon_CV_Portal.Domain.Educations;
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


        public DbSet<CV> CVs { get; set; }
        public DbSet<Vacancie> Vacancies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CvPortalDbContext).Assembly);

            modelBuilder.Entity<ApplicationUserRole>().HasOne(item => item.Role).WithMany(item => item.UserRoles).HasForeignKey(item => item.RoleId);
            modelBuilder.Entity<ApplicationUserRole>().HasOne(item => item.User).WithMany(item => item.UserRoles).HasForeignKey(item => item.UserId);

            //modelBuilder.Entity<FavouriteVacancie>().HasOne(item => item.Vacancie).WithMany(item => item.FavouriteVacancies).HasForeignKey(item => item.VacansyId);
            //modelBuilder.Entity<FavouriteVacancie>().HasOne(item => item.User).WithMany(item => item.FavouriteVacancies).HasForeignKey(item => item.UserId);
            //modelBuilder.Entity<FavouriteVacancie>()
            //             .HasKey(x => x.Id);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(c => c.CV)
                .WithOne(u => u.User)
                .HasForeignKey<CV>(k => k.UserId);

            modelBuilder.Entity<ApplicationUser>()
                .HasOne(v => v.Vacancie)
                .WithOne(u => u.User)
                .HasForeignKey<Vacancie>(k => k.UserId);

            modelBuilder.Entity<CV>()
                .HasMany(e => e.Educations)
                .WithOne(c => c.CV);

            modelBuilder.Entity<CV>()
                .HasMany(s => s.Skills)
                .WithOne(c => c.CV);

            modelBuilder.Entity<CV>()
                .HasMany(w => w.WorkignExperiences)
                .WithOne(c => c.CV);

            modelBuilder.Entity<Education>()
                .HasOne(c => c.CV)
                .WithMany(e => e.Educations);

            modelBuilder.Entity<WorkignExperience>()
               .HasOne(c => c.CV)
               .WithMany(w => w.WorkignExperiences);

            modelBuilder.Entity<Skill>()
               .HasOne(c => c.CV)
               .WithMany(s => s.Skills);

        }
    }
}
