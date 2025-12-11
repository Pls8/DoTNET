using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskInClass.Models;

namespace TaskInClass.Config
{
    public class TasksConfig : IEntityTypeConfiguration<TasksClass>
    {
        public void Configure(EntityTypeBuilder<TasksClass> builder)
        {
            builder.Property(t => t.Name)
                 .IsRequired()
                 .HasMaxLength(70);

            builder.Property(t => t.Description)
                .IsRequired();

            builder.Property(t => t.CreatedAt)
                .HasDefaultValue(DateTime.UtcNow);

            builder.Property(t => t.Deadline)
                .IsRequired();

            builder.Property(t => t.IsCompeleted)
                .HasDefaultValue(false)
                .IsRequired(false);

        }
    }
}
