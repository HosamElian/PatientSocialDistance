using System.ComponentModel.DataAnnotations;

namespace PatientSocialDistance.Persistence.DTOs
{
    public class InteractionDto
    {
        [Required]
        public int VistId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string VistorName { get; set; }
        [Required]
        public int DurationInMinutes { get; set; }
        [Required]
        public string InteractionDate { get; set; }
    }
}
