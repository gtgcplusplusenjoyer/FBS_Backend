using FBS.Core.Entities;
using FBS.Core.Entities.Training;
using FBS.Core.Entities.User;
using FBS.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace FBS.Infrastructure.Context
{
    public class FbsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<News> News { get; set; }
        public FbsDbContext(DbContextOptions<FbsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name).IsRequired()
                .HasMaxLength(100);

                entity.HasIndex(e => e.Email).IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PasswordHash)
                   .IsRequired()
                   .HasMaxLength(256);

                entity.Property(e => e.UserRole)
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .HasDefaultValue(RolesTypes.Visitor);
            });

            modelBuilder.Entity<Workout>(entity =>
            {
                entity.ToTable("Workouts");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Type)
                    .HasConversion<string>()
                    .HasMaxLength(50);

                entity.Property(e => e.Exercises)
                    .HasColumnType("jsonb");

                entity.HasIndex(e => new { e.UserId, e.Date })
                    .HasDatabaseName("IX_Workouts_UserId_Date");

                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.Date);
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.ToTable("News");

                entity.HasKey(e=>e.Id);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.PublishedAt)
                    .IsRequired();

                entity.HasIndex(e => e.PublishedAt)
                    .HasDatabaseName("IX_News_PublishedAt");

                entity.HasIndex(e => e.Title)
                    .HasDatabaseName("IX_News_Title");
            });

        }

    }
}

