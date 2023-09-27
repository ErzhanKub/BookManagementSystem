using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.DataBase
{
    /// <summary>
    /// Represents the database context for the application.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the context.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        /// <summary>
        /// Gets or sets the collection of users.
        /// </summary>
        public DbSet<User> Users => Set<User>();

        /// <summary>
        /// Gets or sets the collection of books.
        /// </summary>
        public DbSet<Book> Books => Set<Book>();

        /// <summary>
        /// Gets or sets the collection of baskets.
        /// </summary>
        public DbSet<Basket> Baskets => Set<Basket>();

        public DbSet<Order> Orders => Set<Order>();

        /// <inheritdoc/>
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
            modelBuilder.Entity<Order>().Navigation(o => o.Books).AutoInclude();
        }
    }

}
