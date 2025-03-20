using PiggyPalWebApp.Models.Database;
using System.ComponentModel.DataAnnotations;

namespace PiggyPalWebApp.Models
{
    public class UserAuthDataModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
