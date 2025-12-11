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


        //// GET: List all tasks
        //public async Task<IActionResult> Index()
        //{
        //    var tasks = await _context.Tasks
        //        .Include(t => t.Category)
        //        .OrderBy(t => t.DeadLine)
        //        .ToListAsync();

        //    return View(tasks);
        //}

        //public IActionResult Create()
        //{
        //    // ALWAYS load categories
        //    ViewBag.Categories = _context.Categories.ToList();
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(TaskClass task)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Tasks.Add(task);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    // IF validation fails, RELOAD categories for the view
        //    ViewBag.Categories = _context.Categories.ToList();  // ← This is missing!
        //    return View(task);
        //}

        //// GET: Delete confirmation
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var task = await _context.Tasks.FindAsync(id);
        //    if (task != null)
        //    {
        //        _context.Tasks.Remove(task);
        //        await _context.SaveChangesAsync();
        //    }
        //    return RedirectToAction("Index");
        //}



        //// GET: Edit form
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var task = await _context.Tasks.FindAsync(id);
        //    if (task == null)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.Categories = await _context.Categories.ToListAsync();
        //    return View(task);
        //}
        //// POST: Update task
        //[HttpPost]
        //public async Task<IActionResult> Edit(TaskClass task)
        //{
        //    _context.Tasks.Update(task);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}
        // GET: List all tasks
        public async Task<IActionResult> Index()
        {
            //Linq oprater in get Category (eager loading)
            var tasks = await _context.Tasks
                .Include(t => t.Category)
                .OrderBy(t => t.DeadLine)
                .ToListAsync();

            return View(tasks);
        }
        // Helper method to load categories
        private void LoadCategories()
        {
            //      v---- this it key
            ViewBag.Categories = _context.Categories.ToList();
        }

        // GET: Create form
        public IActionResult Create()
        {
            LoadCategories();  // Always load categories
            return View();
        }

        // POST: Create task
        [HttpPost]
        public async Task<IActionResult> Create(TaskClass task)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            //If validation fails, reload categories
            LoadCategories();
            return View(task);
        }

        // GET: Edit form
        public async Task<IActionResult> Edit(int id)
        {                                   //^--FromRouteAttribute
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return RedirectToAction("Index");
            }

            LoadCategories();
            return View(task);
        }

        // POST: Update task
        [HttpPost]
        public async Task<IActionResult> Edit(TaskClass task)
        {
            if (ModelState.IsValid)
            {
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            LoadCategories();
            return View(task);
        }

        // GET: Delete task
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
