using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TaskApp.Models;

namespace TaskApp.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API configurations can be added here
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

        public DbSet<TaskClass> Tasks { get; set; }
        public DbSet<TaskCategoryClass> Categories { get; set; }

    }
}
