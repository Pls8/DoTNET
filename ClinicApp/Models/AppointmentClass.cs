using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace ClinicApp.Models
{
    public class AppointmentClass
    {
        [Key]
        public int AppointmentId { get; set; }

        public int PatientId { get; set; }
        public PatientClass Patient { get; set; }

        public int DoctorId { get; set; }
        public DoctorClass Doctor { get; set; }

        public DateTime Date { get; set; }
    }
}
