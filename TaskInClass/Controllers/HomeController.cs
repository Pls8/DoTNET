using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TaskInClass.Context;
using TaskInClass.Models;

namespace TaskInClass.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db = db;

        }

        public IActionResult Index()
        {
            // Eager Loading
            var tasks = _db.Tasks.Include(w => w.taskCategory).ToList();

            return View(tasks);
        }
        public IActionResult Details(int id)
        {
            var todo = _db.Tasks.Include(w => w.taskCategory).FirstOrDefault(t => t.Id == id);
            return View(todo);

        }
        public IActionResult Create()
        {
            var cats = new SelectList(_db.TaskCategories, "Id", "Name");
            //var cats = _db.TaskCategories.ToList();

            ViewBag.CategoriesList = cats;

            return View();
        }

        [HttpPost]
        public IActionResult Create(TasksClass obj)
        {
            if (ModelState.IsValid)
            {
                _db.Tasks.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            var cats = new SelectList(_db.TaskCategories, "Id", "Name");
            ViewBag.CategoriesList = cats;

            return View(obj);
        }

        public IActionResult Update(int id)
        {
            var todo = _db.Tasks.Include(w => w.taskCategory).FirstOrDefault(t => t.Id == id);
            var cats = new SelectList(_db.TaskCategories, "Id", "Name");
            //var cats = _db.TaskCategories.ToList();

            ViewBag.CategoriesList = cats;
            return View(todo);
        }
        [HttpPost]
        public IActionResult Update(TasksClass obj)
        {
            if (ModelState.IsValid)
            {
                _db.Tasks.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            var cats = new SelectList(_db.TaskCategories, "Id", "Name");
            ViewBag.CategoriesList = cats;

            return View(obj);
        }
    }
}
