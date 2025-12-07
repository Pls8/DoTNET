using System.ComponentModel.DataAnnotations;

namespace ClinicApp.Models
{
    public class PatientClass
    {
        [Key]
        public int PatientId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

    }
}
