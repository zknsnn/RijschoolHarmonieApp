using Microsoft.EntityFrameworkCore;
using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.Data
{
    public class RijschoolHarmonieAppContext : DbContext
    {
        public RijschoolHarmonieAppContext() { }

        public RijschoolHarmonieAppContext(DbContextOptions<RijschoolHarmonieAppContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<InstructorPrice> InstructorPrices { get; set; }
        public DbSet<StudentAccount> StudentAccounts { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure one-to-many relationship between InstructorPrice and User (Instructor)
            modelBuilder
                .Entity<InstructorPrice>()
                .HasOne(ip => ip.Instructor) // Each InstructorPrice belongs to one Instructor
                .WithMany(u => u.InstructorPrices) // Each Instructor can have multiple InstructorPrices
                .HasForeignKey(ip => ip.InstructorId) // Foreign key in InstructorPrice table
                .OnDelete(DeleteBehavior.Cascade); // If Instructor is deleted, delete related prices

            modelBuilder
                .Entity<StudentAccount>()
                .Property(sa => sa.Balance)
                .HasComputedColumnSql("[TotalDebit] - [TotalCredit]");

            modelBuilder.Entity<StudentAccount>().HasIndex(sa => sa.StudentId).IsUnique();

            modelBuilder
                .Entity<Payment>()
                .HasOne(p => p.StudentAccount)
                .WithMany(sa => sa.Payments)
                .HasForeignKey(p => p.StudentAccountId)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}
