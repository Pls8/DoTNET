namespace TaskApp.Models
{
    public class TaskCategoryClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<TaskClass> Tasks { get; set; } = new HashSet<TaskClass>();
        
    }
}
