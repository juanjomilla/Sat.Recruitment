using System.ComponentModel.DataAnnotations;

namespace Sat.Recruitment.Api.Models.Request
{
    public class CreateUserRequest
    {
        [Required(ErrorMessage = "The {0} is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        public string Phone { get; set; }

        public string UserType { get; set; }

        public decimal Money { get; set; }
    }
}
