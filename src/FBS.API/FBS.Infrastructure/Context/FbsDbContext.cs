using FBS.Core.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace FBS.Infrastructure.Context
{
    public class FbsDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public FbsDbContext(DbContextOptions<FbsDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "users");
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
            });

        }

    }
}
