using System.ComponentModel.DataAnnotations;

namespace PatientSocialDistance.Persistence.DTOs
{
    public class VisitDto
    {
        [Required]
        public string VisitedUsername { get; set; }
        [Required]
        public string VisitorUsername { get; set; }
        [Required]
        public string VisitDate { get; set; }
        [Required]
        //public string? Reason { get; set; }
        public string? Message { get; set; }
    }
}
