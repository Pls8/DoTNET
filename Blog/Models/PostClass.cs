using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class PostClass
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; } // green underline, by defualt string not-null
        public string MediaURL { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(category))]
        public int categoryId { get; set; }
        public CategoryClass category { get; set; }

    }
}
