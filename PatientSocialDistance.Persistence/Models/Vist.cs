using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientSocialDistance.Persistence.Models
{
    public class Vist
    {
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool Approved { get; set; }
        public int VistStatusId { get; set; }
        public string VistedUserId { get; set; }
        public string VistorUserId { get; set; }
        [Required]
        public DateTime VistDate { get; set; }
        public string Reason { get; set; }
        public string Message { get; set; }
        
        [ForeignKey(nameof(VistStatusId))]
        public VistApprovalStatus VistStatus { get; set; }
        [ForeignKey(nameof(VistedUserId))]
        public ApplicationUser VistedUser { get; set; }
        [ForeignKey(nameof(VistorUserId))]
        public ApplicationUser VistorUser { get; set; }
    }
}
