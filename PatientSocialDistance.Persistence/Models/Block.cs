using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientSocialDistance.Persistence.Models
{
    public class Block
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public string UserMakeBlockId { get; set; }
        public string UserBlockedId { get; set; }
        public DateTime BlockedAt { get; set; }

        [ForeignKey("UserMakeBlockId")]
        public ApplicationUser UserMakeBlock { get; set; }

        [ForeignKey("UserBlockedId")]
        public ApplicationUser UserBlocked { get; set; }
    }
}
