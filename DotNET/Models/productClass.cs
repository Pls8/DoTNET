namespace DotNET.Models
{
    public class productClass
    {
        public int Id { get; set; } // in ASP.NET , 'Id' is conventionally used as the primary key
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public decimal? Amount { get; set; }
        public bool InStock { get; set; }

    }
}
