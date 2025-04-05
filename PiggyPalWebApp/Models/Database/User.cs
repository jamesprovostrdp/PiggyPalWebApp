using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PiggyPalWebApp.Models.Database
{
    public class User : IdentityUser
    {
        public ICollection<Category> Categories { get; set; }
    }
}
