using ClinicApp.ApplicationDbContext;
using ClinicApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;
using Microsoft.EntityFrameworkCore;


namespace ClinicApp.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly AppDbContext _context;

        public DoctorsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View(_context.Doctors.ToList());

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(DoctorClass doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Doctors.Add(doctor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        public IActionResult Search(string specialty)
        {
            if (string.IsNullOrWhiteSpace(specialty))
                return View("Index", new List<DoctorClass>());

            var results = _context.Doctors
                .Where(d => d.Specialty != null &&
                            d.Specialty.ToLower().Contains(specialty.ToLower()))
                .ToList();

            return View("Index", results);
        }

    }
}
