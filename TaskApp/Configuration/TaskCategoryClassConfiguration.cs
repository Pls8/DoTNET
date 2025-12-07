using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskApp.Models;

namespace TaskApp.Configuration
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
