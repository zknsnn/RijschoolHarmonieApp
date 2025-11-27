using Microsoft.EntityFrameworkCore;
using RijschoolHarmonieApp.Models;

namespace RijschoolHarmonieApp.Data
{
    public class RijschoolHarmonieAppContext : DbContext
    {
        public RijschoolHarmonieAppContext(DbContextOptions<RijschoolHarmonieAppContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
