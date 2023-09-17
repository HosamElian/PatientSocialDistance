using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientSocialDistance.Persistence.Models
{
    public class Interaction
    {
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
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
        [ForeignKey(nameof(VistId))]
        public Vist Vist { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
        
        [ForeignKey(nameof(VistorUserId))]
        public ApplicationUser VistorUser { get; set; }

    }
}
