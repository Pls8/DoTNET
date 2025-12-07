using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskApp.Context;
using TaskApp.Models;

namespace TaskApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly AppDbContext _context;
        public TaskController(AppDbContext context)
        {
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}


        // GET: List all tasks
        public async Task<IActionResult> Index()
        {
            var tasks = await _context.Tasks
                .Include(t => t.Category)
                .OrderBy(t => t.DeadLine)
                .ToListAsync();

            return View(tasks);
        }

        // GET: Create form
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        // POST: Create task
        [HttpPost]
        public async Task<IActionResult> Create(TaskClass task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Delete confirmation
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }



    }
}
