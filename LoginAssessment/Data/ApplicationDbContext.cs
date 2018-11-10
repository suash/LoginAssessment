using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoginAssessment.Data
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginDevice>()
                .Property(p => p.Name).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<LoginReason>()
                .Property(p => p.Name).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<LoginPreference>()
                .Property(p => p.Name).IsRequired().HasMaxLength(50);
        }*/
    }
}