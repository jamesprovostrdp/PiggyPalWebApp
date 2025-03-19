using System.ComponentModel.DataAnnotations;

namespace PiggyPalWebApp.Models.Database
{
    public enum Role
    {
        User,
        Admin
    }

    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Not valid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string HashPassword { get; set; }

        public Role Role { get; set; } = Role.User;

    }
}
