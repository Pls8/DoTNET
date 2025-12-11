using System.ComponentModel.DataAnnotations.Schema;

namespace TaskInClass.Models
{
    public class TasksClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsCompeleted { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(taskCategory))]
        public int TaskCategoryId { get; set; }
        public TaskCategoryClass? taskCategory { get; set; }
                            //  ^-- this is importnat 
    }
}
