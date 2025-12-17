using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TasksAPI.Models;

namespace TasksAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<TaskClass> Tasks { get; set; }
        public DbSet<TaskCategoryClass> Categories { get; set; }
    }
}
