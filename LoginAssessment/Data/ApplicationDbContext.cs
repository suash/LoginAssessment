using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LoginAssessment.Data;

namespace LoginAssessment.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginDevice>()
                .HasKey(p => p.Id).ValueGeneratedNever()
                .Property(p => p.Name).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<LoginReason>()
                .HasKey(p => p.Id).ValueGeneratedNever()
                .Property(p => p.Name).IsRequired().HasMaxLength(50);
            
            modelBuilder.Entity<LoginPreference>()
                .HasKey(p => p.Id).ValueGeneratedNever()
                .Property(p => p.Name).IsRequired().HasMaxLength(50);
        }
    }
}