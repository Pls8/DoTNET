using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasksAPI.Models;

namespace TasksAPI.Config
{
    public class TaskCategoryClassConfiguration : IEntityTypeConfiguration<TaskCategoryClass>
    {
        public void Configure(EntityTypeBuilder<TaskCategoryClass> builder)
        {
            builder.HasMany<TaskClass>(t => t.Tasks)
                   .WithOne(c => c.Category)
                   .HasForeignKey(c => c.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
