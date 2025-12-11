using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskApp.Context;
using TaskApp.Models;

namespace TaskApp.Controllers
{
    public class CategoryController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //readonly like const
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }



        // GET: List all categories
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }

        // GET: Create form
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create category
        [HttpPost]
        public async Task<IActionResult> Create(TaskCategoryClass category)
        {
            if(ModelState.IsValid)
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }  
            return View(category);

        }

        // GET: Delete
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }


        // GET: Edit category
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // POST: Update category
        [HttpPost]
        public async Task<IActionResult> Edit(TaskCategoryClass category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
