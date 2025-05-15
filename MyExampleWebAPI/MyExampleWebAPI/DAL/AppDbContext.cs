using Microsoft.EntityFrameworkCore;

namespace MyExampleWebAPI.Models
{
    public class AppDbContext : DbContext
    {
        // Intiialized EF DB Context with our base AppDbContext context object
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Our Tables DTO objects classess based on our models
        public DbSet<Member> MembersDTO { get; set; }
        public DbSet<WeightEntry> WeightEntriesDTO { get; set; }

        // Additional options and configuration during on model creating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Member>()
                .Property(m => m.CurrentWeight)
                .HasPrecision(5, 1); 

            modelBuilder.Entity<Member>()
                .Property(m => m.Height)
                .HasPrecision(5, 2); 

            modelBuilder.Entity<Member>()
                .Property(m => m.BMI)
                .HasPrecision(4, 1); 

            modelBuilder.Entity<WeightEntry>()
                .Property(w => w.Weight)
                .HasPrecision(5, 1); 
        }

    }
}
