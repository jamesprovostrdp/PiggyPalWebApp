using System.ComponentModel.DataAnnotations; // Uses DataAnnotations such as Key and Required

namespace PiggyPalWebApp.Models
{
    public class Budget
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public decimal Income { get; set; }  // Users monthly or weekly income

        [Required]
        public decimal ExpectedSavings { get; set; } // Desired savings amount

        public decimal AvailableBudget => Income - ExpectedSavings;  // Remaining budget for expenses
    }
}