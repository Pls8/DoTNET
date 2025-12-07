using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Runtime.Intrinsics.X86;

namespace Blog.Models
{
    public class CategoryClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<PostClass> posts { get; set; } = new HashSet<PostClass>();

        //____________________________________________________________.
        //| Scenario                             | Use                |
        //| ------------------------------------ | ------------------ |
        //| Need to add/remove items             | **ICollection<T>** |
        //| EF Core navigation property          | **ICollection<T>** |
        //| Just need to read/iterate            | **IEnumerable<T>** |
        //| Expose read-only list in API         | **IEnumerable<T>** |
        //| You don’t want to allow modification | **IEnumerable<T>** |
        //-----------------------------------------------------------/
        //For EF entity classes → Use ICollection<T>
        //For read-only access, APIs, or LINQ → Use IEnumerable<T>

    }
}
