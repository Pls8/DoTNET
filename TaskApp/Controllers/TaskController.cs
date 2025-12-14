using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskApp.Context;
using TaskApp.Models;
using TaskApp.Models.AuthModel;

namespace TaskApp.Controllers
{
    [Authorize] // protect all actions
    public class TaskController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public TaskController(AppDbContext context, UserManager<AppUser> userManager)
        {   // Initialize
            _context = context;
            _userManager = userManager;
        }
        #region unused code
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
        #endregion


        // GET: List all tasks
        public async Task<IActionResult> Index()
        {
            ////Linq oprater in get Category (eager loading)
            //var tasks = await _context.Tasks
            //    .Include(t => t.Category)
            //    .OrderBy(t => t.DeadLine)
            //    .ToListAsync();
            //return View(tasks);

            // Get current user's ID
            var userId = _userManager.GetUserId(User);
            if (userId == null) return RedirectToAction("Login", "Account");
            // THE FILTER!
            var userTask = await _context.Tasks
            .Include(t => t.Category)            
            .Where(t => t.UserId == userId) // <-- FILTER
            .OrderBy(t => t.DeadLine)
            .ToListAsync();

            return View(userTask);
        }
        // Helper method to load categories
        //private void LoadCategories()
        //{
        //    //      v---- this it key
        //    ViewBag.Categories = _context.Categories.ToList();
        //}

        // GET: Create form
        public IActionResult Create()
        {
            //LoadCategories();  // Always load categories
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        // POST: Create task
        [HttpPost]
        public async Task<IActionResult> Create(TaskClass task)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");
            task.UserId = user.Id;
            if (ModelState.IsValid)
            {
                //task.UserId = _userManager.GetUserId(User); // Assign current user's ID

                _context.Tasks.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Categories = _context.Categories.ToList();
            return View(task);

            #region debug version
            //// Debug: Print what's in the task object
            //Console.WriteLine($"Task Name: {task.Name}");
            //Console.WriteLine($"Task DeadLine: {task.DeadLine}");
            //Console.WriteLine($"Task CategoryId: {task.CategoryId}");

            //// Get current user
            //var user = await _userManager.GetUserAsync(User);
            //if (user == null)
            //{
            //    Console.WriteLine("User is NULL - redirecting to login");
            //    return RedirectToAction("Login", "Account");
            //}

            //Console.WriteLine($"Current user ID: {user.Id}");

            //// Check ModelState
            //if (!ModelState.IsValid)
            //{
            //    Console.WriteLine("ModelState is NOT valid!");
            //    foreach (var key in ModelState.Keys)
            //    {
            //        var errors = ModelState[key].Errors;
            //        foreach (var error in errors)
            //        {
            //            Console.WriteLine($"Error for {key}: {error.ErrorMessage}");
            //        }
            //    }

            //    ViewBag.Categories = _context.Categories.ToList();
            //    return View(task);
            //}

            //Console.WriteLine("ModelState is valid - proceeding to create task");

            //// Set the UserId for the task
            //task.UserId = user.Id;

            //try
            //{
            //    _context.Tasks.Add(task);
            //    var result = await _context.SaveChangesAsync();
            //    Console.WriteLine($"SaveChangesAsync result: {result} rows affected");

            //    if (result > 0)
            //    {
            //        Console.WriteLine("Task created successfully!");
            //    }

            //    return RedirectToAction("Index");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"Error saving task: {ex.Message}");
            //    Console.WriteLine($"Inner exception: {ex.InnerException?.Message}");

            //    ViewBag.Categories = _context.Categories.ToList();
            //    ModelState.AddModelError("", "Error saving task to database.");
            //    return View(task);
            //}
            #endregion
        }

        // GET: Edit form
        public async Task<IActionResult> Edit(int id)
        {                                   //^--FromRouteAttribute
            // Get current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            // Get task and ensure it belongs to current user
            var task = await _context.Tasks
                .Where(t => t.Id == id && t.UserId == user.Id)  // <-- Check ownership
                .FirstOrDefaultAsync();

            if (task == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(task);
        }

        // POST: Update task
        [HttpPost]
        public async Task<IActionResult> Edit(TaskClass task)
        {
            // Get current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            // Ensure task belongs to current user
            var existingTask = await _context.Tasks
                .Where(t => t.Id == task.Id && t.UserId == user.Id)
                .FirstOrDefaultAsync();

            if (existingTask == null)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                // Update properties but keep the original UserId
                existingTask.Name = task.Name;
                existingTask.DeadLine = task.DeadLine;
                existingTask.CategoryId = task.CategoryId;

                _context.Tasks.Update(existingTask);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = await _context.Categories.ToListAsync();
            return View(task);
        }

        // GET: Delete task
        public async Task<IActionResult> Delete(int id)
        {
            // Get current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            // Get task and ensure it belongs to current user
            var task = await _context.Tasks
                .Where(t => t.Id == id && t.UserId == user.Id)
                .FirstOrDefaultAsync();

            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }


        


    }
}
