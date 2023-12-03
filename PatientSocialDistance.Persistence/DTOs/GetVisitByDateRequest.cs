using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientSocialDistance.Persistence.DTOs
{
    public class GetVisitByDateRequest
    {
        public string Username { get; set; }
        public bool IsApproved { get; set; }
        public string Date { get; set; }
    }
}
