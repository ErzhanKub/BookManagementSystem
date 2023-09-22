using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.DataBase
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<User> Users => Set<User>();
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Basket> Baskets => Set<Basket>();
        public DbSet<Role> Roles => Set<Role>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "user" },
                new Role { Id = 2, Name = "admin" }
            );
        }
    }
}
