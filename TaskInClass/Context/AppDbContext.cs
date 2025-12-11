using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TaskInClass.Models;

namespace TaskInClass.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<TasksClass> Tasks { get; set; }
        public DbSet<TaskCategoryClass> TaskCategories { get; set; }
    }
}
