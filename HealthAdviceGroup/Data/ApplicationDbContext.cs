using HealthAdviceGroup.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HealthAdviceGroup.Models;

namespace HealthAdviceGroup.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Apply custom configuration for ApplicationUser entity
            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        }

        public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
        {
            // Configure method to set constraints for ApplicationUser properties
            public void Configure(EntityTypeBuilder<ApplicationUser> builder)
            {
                // Set maximum lengths for FirstName and LastName properties
                builder.Property(u => u.FirstName).HasMaxLength(20);
                builder.Property(u => u.LastName).HasMaxLength(20);
            }
        }

        // DbSet for the Health, Advice, and Save entity in the database
        public DbSet<HealthAdviceGroup.Models.Health> Health { get; set; } = default!;

        public DbSet<HealthAdviceGroup.Models.Advice> Advice { get; set; } = default!;
        public DbSet<HealthAdviceGroup.Models.Save> Save { get; set; } = default!;
    }
}
