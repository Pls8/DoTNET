using System.ComponentModel.DataAnnotations;

namespace ClinicApp.Models
{
    public class DoctorClass
    {
        [Key]
        public int DoctorId { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
    }
}
