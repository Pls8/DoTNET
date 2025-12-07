using ClinicApp.ApplicationDbContext;
using ClinicApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Controllers
{
    public class PatientsController : Controller
    {
        private readonly AppDbContext _context;

        public PatientsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Patients.ToList());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(PatientClass patient)
        {
            if (ModelState.IsValid)
            {
                _context.Patients.Add(patient);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(patient);
        }
    }
}
