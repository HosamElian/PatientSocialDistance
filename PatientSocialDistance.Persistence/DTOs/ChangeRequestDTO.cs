using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientSocialDistance.Persistence.DTOs
{
    public class ChangeRequestDTO
    {
        public int VisitId { get; set; }
        public int NotificationId { get; set; }
        public bool IsAccepted { get; set; }
    }
}
