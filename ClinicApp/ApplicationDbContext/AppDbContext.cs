using ClinicApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace ClinicApp.ApplicationDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
                                            // ^--- <> genric is for multiple db
        public DbSet<PatientClass> Patients { get; set; }
        public DbSet<DoctorClass> Doctors { get; set; }
        public DbSet<AppointmentClass> Appointments { get; set; }
    }
}
