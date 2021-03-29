using System.Text.RegularExpressions;
using Sat.Recruitment.Api.Models.Request;

namespace Sat.Recruitment.Api.Models
{
    public class User
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string UserType { get; set; }

        public decimal Money { get; set; }

        public User() { }

        public User(CreateUserRequest userRequest)
        {
            Name = userRequest.Name;
            Email = Regex.Replace(userRequest.Email, @"\.(?=.*?@)|\+(.*?[^@]*)", string.Empty);
            Address = userRequest.Address;
            Phone = userRequest.Phone;
            UserType = userRequest.UserType;
            Money = userRequest.Money;
        }
    }
}
