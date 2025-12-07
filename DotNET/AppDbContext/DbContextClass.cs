using DotNET.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNET.AppDbContext
{
    public class DbContextClass : DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer();
        //}


        // dependency injection constructor
        public DbContextClass(DbContextOptions<DbContextClass> options) : base(options)
        {                                       // ^--- <> genric is for multiple db
        }

        public DbSet<productClass> products { get; set; }
    }
}
