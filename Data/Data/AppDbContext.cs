using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = Guid.Parse("2EA08C6E-BB67-4E5E-9986-1BE063CE4FF5"),
                    RoleName = "Пользователь"
                },
                new Role
                {
                    Id = Guid.Parse("7F82EFCF-D593-40CB-A9CE-255069B8A397"),
                    RoleName = "Администратор"
                }
                );
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Test> Tests { get; set; }
    }
}
