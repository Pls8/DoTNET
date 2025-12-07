using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Config
{
    //1.
    //Create Folder with Configrations name> that will config each model class
    //insted of implment OnModelCreation in AppDbContext !
    //this Fulent Api for each Model(Class/Table)
    public class PostConfig : IEntityTypeConfiguration<PostClass> // ctrl + . to implment config interface
    {
        public void Configure(EntityTypeBuilder<PostClass> builder)
        {
            builder.Property(p => p.CreatedAt).HasDefaultValue(DateTime.Now);
            // ^-- this will convert time zone

            builder.HasOne<CategoryClass>(w => w.category)
                .WithMany().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
