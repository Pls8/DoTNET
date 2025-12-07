using ClinicApp.ApplicationDbContext;
using ClinicApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicApp.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly AppDbContext _context;

        public AppointmentsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var apps = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToList();

            return View(apps);
        }

        public IActionResult Create()
        {
            ViewBag.Patients = _context.Patients.ToList();
            ViewBag.Doctors = _context.Doctors.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(AppointmentClass app)
        {
            if (ModelState.IsValid)
            {
                _context.Appointments.Add(app);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(app);
        }

        public IActionResult PatientAppointments(int id)
        {
            var apps = _context.Appointments
                            .Include(a => a.Doctor)
                            .Include(a => a.Patient)
                            .Where(a => a.PatientId == id)
                            .ToList();

            return View(apps);
        }
    }
}
