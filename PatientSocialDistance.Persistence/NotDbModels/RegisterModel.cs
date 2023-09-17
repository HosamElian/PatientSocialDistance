using System.ComponentModel.DataAnnotations;

namespace PatientSocialDistance.Persistence.NotDbModels
{
    public class RegisterModel
    {
        [Required]
        public int UserTypeId { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string NationalId { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public string Hospital { get; set; }
    }
}
