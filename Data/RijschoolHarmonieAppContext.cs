using Microsoft.EntityFrameworkCore;
using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.Data
{
    public class RijschoolHarmonieAppContext : DbContext
    {
        public RijschoolHarmonieAppContext(DbContextOptions<RijschoolHarmonieAppContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<InstructorPrice> InstructorPrices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure one-to-many relationship between InstructorPrice and User (Instructor)
            modelBuilder
                .Entity<InstructorPrice>()
                .HasOne(ip => ip.Instructor) // Each InstructorPrice belongs to one Instructor
                .WithMany(u => u.InstructorPrices) // Each Instructor can have multiple InstructorPrices
                .HasForeignKey(ip => ip.InstructorId) // Foreign key in InstructorPrice table
                .OnDelete(DeleteBehavior.Cascade); // If Instructor is deleted, delete related prices

            base.OnModelCreating(modelBuilder);
        }
    }
}
