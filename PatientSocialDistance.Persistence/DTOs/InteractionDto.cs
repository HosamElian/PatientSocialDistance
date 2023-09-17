using System.ComponentModel.DataAnnotations;

namespace PatientSocialDistance.Persistence.DTOs
{
    public class InteractionDto
    {
        [Required]
        public int VistId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string VistorUserId { get; set; }
        [Required]
        public int DurationInMinutes { get; set; }
        [Required]
        public DateTime InteractionDate { get; set; }
    }
}
