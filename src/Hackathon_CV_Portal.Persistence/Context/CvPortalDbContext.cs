using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Hackathon_CV_Portal.Persistence.Context
{
    public class CvPortalDbContext : IdentityDbContext
    {
        public CvPortalDbContext(DbContextOptions<CvPortalDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CvPortalDbContext).Assembly);
        }
    }
}
