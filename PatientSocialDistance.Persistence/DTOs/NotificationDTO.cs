using PatientSocialDistance.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientSocialDistance.Persistence.DTOs
{
    public class NotificationDTO
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string? TargetUsername { get; set; }
        public string? MakeActionUsername{ get; set; }
        public bool IsChangeDate { get; set; }
        public int? VisitId { get; set; }
        public bool? ChangeAccepted { get; set; }
    }
}
