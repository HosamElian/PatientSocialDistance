using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientSocialDistance.Persistence.DTOs
{
    public class VisitsAcceptedDTO
    {
        public string VisitedUsername { get; set; }
        public string VisitorUsername { get; set; }
        public string? Message { get; set; }
        public string StartDate { get; set; }
        public string StartTime { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
