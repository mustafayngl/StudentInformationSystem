using Microsoft.EntityFrameworkCore;

namespace StudentInformationSystem.Models
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }

        // Add DbSet properties for other entities if needed
    }
}