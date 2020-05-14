using System.ComponentModel.DataAnnotations;

namespace Cloudbass.Utilities
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public bool Active { get; set; }


        [Required]
        public string Password { get; set; }
    }
}