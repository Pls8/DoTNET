using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskApp.Models.AuthModel;

namespace TaskApp.Models
{
    public class TaskClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DeadLine { get; set; }

        //public bool IsComplete { get; set; } = false;

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public TaskCategoryClass? Category { get; set; }
        = null;          //     ^-- this ? mark is VERY Important

        // ===== NEW: Foreign Key to User =====
        [ForeignKey(nameof(User))]
        public string? UserId { get; set; }        
        public virtual AppUser? User { get; set; }

    }
}
