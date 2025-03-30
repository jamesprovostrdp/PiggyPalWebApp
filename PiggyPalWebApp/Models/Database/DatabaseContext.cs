using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PiggyPalWebApp.Models.Database
{
    public class DatabaseContext : IdentityDbContext<User>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Record> Records { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    DisplayName = "Food",
                    SpendingLimit = 10,
                    UserID = 1,
                }
            );

            modelBuilder.Entity<Record>().HasData(
                new Record
                {
                    RecordId = 1,
                    CategoryId = 1,
                    RecordAmount = 2.50,
                    DateOfRecord = DateTime.Today
                },
                new Record
                {
                    RecordId = 2,
                    CategoryId = 1,
                    RecordAmount = 5.50,
                    DateOfRecord = DateTime.Today
                },
                new Record
                {
                    RecordId = 3,
                    CategoryId = 1,
                    RecordAmount = 8.50,
                    DateOfRecord = DateTime.Today
                }
            );

        }
    }
}
