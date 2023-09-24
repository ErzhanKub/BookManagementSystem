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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasIndex(b => b.Title)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
            modelBuilder.Entity<User>().Navigation(u => u.Basket).AutoInclude();
            modelBuilder.Entity<Basket>().Navigation(b => b.Books).AutoInclude();
        }
    }
}
