using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ReviewAndRatingService.Model;

namespace ReviewAndRatingService.Data
{
    public class ReviewContext : DbContext
    {
        public ReviewContext(DbContextOptions<ReviewContext> options) : base(options) { }

        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Review>().HasData(
                new Review { Id = 1, ReviewerName = "John Doe", Content = "Great product!", Rating = 5, CreatedAt = DateTime.UtcNow },
                new Review { Id = 2, ReviewerName = "Jane Smith", Content = "Not bad.", Rating = 3, CreatedAt = DateTime.UtcNow }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }
    }
}
