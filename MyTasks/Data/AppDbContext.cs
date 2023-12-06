using Microsoft.EntityFrameworkCore;
using MyTasks.Models;

namespace MyTasks.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<School> Schools { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Director> Directors { get; set; }
    }
}
