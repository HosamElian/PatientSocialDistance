using System.ComponentModel.DataAnnotations;

namespace PatientSocialDistance.Persistence.DTOs
{
    public class VisitDto
    {
        [Required]
        public string VistedUserId { get; set; }
        [Required]
        public string VistorUserId { get; set; }
        [Required]
        public DateTime VistDate { get; set; }
        [Required]
        public string Reason { get; set; }
        public string? Message { get; set; }
    }
}
