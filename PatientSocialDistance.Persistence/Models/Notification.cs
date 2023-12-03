using System.ComponentModel.DataAnnotations.Schema;

namespace PatientSocialDistance.Persistence.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message  { get; set; }
        public string TargetUserId { get; set; }
        public ApplicationUser TargetUser { get; set; }
        public string UserMakeActionId { get; set; }
        public ApplicationUser UserMakeAction { get; set; }
        public bool IsChangeDate { get; set; }
        public int? VisitId { get; set; }
        public bool? ChangeAccepted { get; set; }
        public bool IsDeleted { get; set; }
    }
}
