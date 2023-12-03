using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientSocialDistance.Persistence.DTOs
{
    public class GetVisitRequest
    {
        public string Username { get; set; }
        public bool IsApproved { get; set;}
    }
}
