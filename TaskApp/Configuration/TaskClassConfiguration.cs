using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskApp.Models;

namespace TaskApp.Configuration
{
    public class TaskClassConfiguration : IEntityTypeConfiguration<TaskClass>
    {
        public void Configure(EntityTypeBuilder<Models.TaskClass> builder)
        {
            //// Foreign Key configuration
            //builder.Property(t => t.CategoryId)
            //    .IsRequired();

            //// Relationship (already configured in Category, but can be defined here too)
            //builder.HasOne(t => t.Category)
            //    .WithMany(c => c.Tasks)
            //    .HasForeignKey(t => t.CategoryId)
            //    .OnDelete(DeleteBehavior.Cascade);
            
            
            
            
            
            
            ////Seed data (optional)
            //builder.HasData(
            //    new TaskClass
            //    {
            //        Id = 1,
            //        Name = "Complete project report",
            //        DeadLine = DateTime.Now.AddDays(3),
            //        CategoryId = 1
            //    },
            //    new TaskClass
            //    {
            //        Id = 2,
            //        Name = "Buy groceries",
            //        DeadLine = DateTime.Now.AddDays(1),
            //        CategoryId = 3
            //    }
        }
    }
}
