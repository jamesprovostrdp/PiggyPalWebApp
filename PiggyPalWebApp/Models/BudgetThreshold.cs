using System.ComponentModel.DataAnnotations; // Uses DataAnnotations such as Key and Required

namespace PiggyPalWebApp.Models
{
    // BudgetThreshhold class that gets and sets user data for budgeting
    public class BudgetThreshold
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public decimal LimitAmount { get; set; } // Sets the budgeting limit

        public decimal CurrentSpending { get; set; } = 0; // Sets the users current weekly/monthly spending based on input criteria
    }
}