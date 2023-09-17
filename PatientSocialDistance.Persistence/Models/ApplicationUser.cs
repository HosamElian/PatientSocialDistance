using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientSocialDistance.Persistence.Models
{
   
    public class ApplicationUser: IdentityUser
    {
        public int UserTypeId { get; set; }
        public int Age { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string NationalId { get; set; }
        public string Nationality { get; set; }
        public string Hospital { get; set; }
        public UserType UserType { get; set; }
        public List<ApplicationUser> BlockedUsers { get; set; }
    }
    
}
