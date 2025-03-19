using PiggyPalWebApp.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace PiggyPalWebApp.Models
{
    public class UserRegisterDataModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public Role Role { get; set; }
    }
}
