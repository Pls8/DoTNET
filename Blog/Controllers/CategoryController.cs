using Blog.Context;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class CategoryController : Controller
    {
        // to add data to table
        // > SQL Server Object Exploere > right-click > View Data

        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //access URL /Category/Index
            var categories = _context.category.ToList();
            return View(categories);
        }

        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
