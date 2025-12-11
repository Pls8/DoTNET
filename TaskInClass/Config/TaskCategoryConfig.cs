using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskInClass.Models;

namespace TaskInClass.Config
{
    public class TaskCategoryConfig : IEntityTypeConfiguration<TaskCategoryClass>
    {
        public void Configure(EntityTypeBuilder<TaskCategoryClass> builder)
        {
            builder.Property(c => c.CreatedAt)
                 .HasDefaultValue(DateTime.UtcNow);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(50);

        }
    }
}
