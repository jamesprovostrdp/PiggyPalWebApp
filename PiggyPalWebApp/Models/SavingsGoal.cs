using System.ComponentModel.DataAnnotations;

namespace PiggyPalWebApp.Models
{
    public class SavingsGoal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public decimal TargetAmount { get; set; }  // Goal target amount

        public decimal CurrentAmount { get; set; } = 0;  // Amount saved so far

        [Required]
        public DateTime Deadline { get; set; }  // Deadline for the goal

        public string Status { get; set; } = "In Progress";  // Default status when working on a goal
    }
}
