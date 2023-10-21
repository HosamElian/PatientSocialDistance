using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientSocialDistance.Persistence.DTOs
{
    public class BlockedUserDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Hospital { get; set; }
        public string PhoneNumber { get; set; }
    }
}
