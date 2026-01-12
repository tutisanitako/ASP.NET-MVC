using AcademicResourceManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AcademicResourceManagement.Persistence.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<LearningResource> LearningResources { get; set; }
        public DbSet<ResourceLog> ResourceLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure LearningResource relationships
            builder.Entity<LearningResource>()
                .HasOne(r => r.UploadedBy)
                .WithMany(u => u.UploadedResources)
                .HasForeignKey(r => r.UploadedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<LearningResource>()
                .HasOne(r => r.ApprovedBy)
                .WithMany(u => u.ApprovedResources)
                .HasForeignKey(r => r.ApprovedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure ResourceLog relationships
            builder.Entity<ResourceLog>()
                .HasOne(l => l.Resource)
                .WithMany(r => r.Logs)
                .HasForeignKey(l => l.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ResourceLog>()
                .HasOne(l => l.PerformedBy)
                .WithMany(u => u.PerformedLogs)
                .HasForeignKey(l => l.PerformedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes for performance
            builder.Entity<LearningResource>()
                .HasIndex(r => r.Status);

            builder.Entity<LearningResource>()
                .HasIndex(r => r.UploadedByUserId);

            builder.Entity<ResourceLog>()
                .HasIndex(l => l.ResourceId);

            builder.Entity<ResourceLog>()
                .HasIndex(l => l.Timestamp);
        }
    }
}