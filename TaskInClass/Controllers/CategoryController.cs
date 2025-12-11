using Microsoft.AspNetCore.Mvc;
using TaskInClass.Context;
using TaskInClass.Models;

namespace TaskInClass.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;
        public CategoryController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var cats = _db.TaskCategories.ToList();
            return View(cats);
        }
        public IActionResult Detail(int Id)
        {
            var cat = _db.TaskCategories.Find(Id);
            return View(cat);

        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TaskCategoryClass cat)
        {
            if (ModelState.IsValid)
            {
                _db.TaskCategories.Add(cat);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cat);

        }

        public IActionResult Update(int Id)
        {
            var cat = _db.TaskCategories.Find(Id);

            return View(cat);
        }
        [HttpPost]
        public IActionResult Update(TaskCategoryClass cat)
        {
            //var ca = _db.TaskCategories.Find(cat.Id);
            _db.TaskCategories.Update(cat);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int Id)
        {
            var cat = _db.TaskCategories.Find(Id);
            _db.TaskCategories.Remove(cat);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
