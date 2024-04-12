using Microsoft.EntityFrameworkCore;

namespace MacroTracker.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Food>? Foods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Food>().Property(f => f.Carbs).HasPrecision(18, 2);
            modelBuilder.Entity<Food>().Property(f => f.Fat).HasPrecision(18, 2);
            modelBuilder.Entity<Food>().Property(f => f.Protein).HasPrecision(18, 2);
        }

    }

}
