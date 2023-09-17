using System.ComponentModel.DataAnnotations;

namespace PatientSocialDistance.Persistence.Models
{
    public class UserType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

    }
}