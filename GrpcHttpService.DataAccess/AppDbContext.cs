using GrpcHttpService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GrpcHttpService.DataAccess
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Organization>()
                .HasMany(o => o.Users)
                .WithMany(u => u.Organizations);

            base.OnModelCreating(builder);
        }
    }
}
