using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace Blog.Context
{
    public class ApplicationDbContext : DbContext //don't name folder name with this
    {
        //_______________________________Instruction_________________________________________________
        // In case of multiple project in same solution you need to run like this for migration      |
        // Add-Migration <MigrationName> -Project <ProjectName> -StartupProject <StartupProjectName> |
        // [ PM> Add-Migration InitialCreate -Project E-Shop -StartupProject E-Shop ]                |
        // [ PM> Update-Database -Project "E-Shop" -StartupProject "E-Shop" ]                        |
        // [ PM> Remove-Migration -Project E-Shop ]                                                  |
        //-------------------------------------------------------------------------------------------
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {                                            // ^--- add this <> to make sure
                                                     // diffrent with multiple DB connecntion 
        }


        //Fluent Api
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //2.Apply Config Folder staff, this will search for implmenation of interface
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());



            ////            v--Table name
            //modelBuilder.Entity<PostClass>()
            //    .Property(p=>p.LikeCount).HasDefaultValue(0).HasMaxLength(100);
        }

        // this one no need if there config for model, and if there relationship with other model it will be implemnt Auto
        public DbSet<PostClass> post { get; set; }
        public DbSet<CategoryClass> category { get; set; }

    }
}
