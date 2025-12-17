using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TasksAPI.Models;

namespace TasksAPI.Config
{
    public class TaskClassConfiguration : IEntityTypeConfiguration<TaskClass>
    {
        public void Configure(EntityTypeBuilder<TaskClass> builder)
        {
            // Foreign Key configuration
            builder.Property(t => t.CategoryId)
                .IsRequired();

            // Relationship (already configured in Category, but can be defined here too)
            builder.HasOne(t => t.Category)
                .WithMany(c => c.Tasks)
                .HasForeignKey(t => t.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
