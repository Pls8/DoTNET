using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TaskApp.Models;
using TaskApp.Models.AuthModel;

namespace TaskApp.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {                           // ^-- 1.changed DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            //2.after adding IdentityDbContext 
            base.OnModelCreating(modelBuilder);

            // Fluent API configurations can be added here
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<TaskClass> Tasks { get; set; }
        public DbSet<TaskCategoryClass> Categories { get; set; }
        public DbSet<AppUser> appUsers { get; set; }

    }
}
