namespace TaskInClass.Models
{
    public class TaskCategoryClass
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public IEnumerable<TasksClass> tasks { get; set; } = new HashSet<TasksClass>();
    }
}
