using System.ComponentModel.DataAnnotations;

namespace PiggyPalWebApp.Models.Database
{
    public class Goal // Budget Goals class
    {
        [Key] //GoalId set as the primary key
        public int GoalId { get; set; }

        public int UserId { get; set; }
        public User? Owner { get; set; } // Nullable value for goal Owner

        [Required]
        [StringLength(25, MinimumLength = 1, ErrorMessage = "Name must be between 1 to 25 characters.")] // Name of the goal contains a character limit of 25
        public string GoalName { get; set; } = "Goal";

        [Required]
        [DataType(DataType.Currency)]
        [Range(1, double.MaxValue, ErrorMessage = "Goal amount must be at least $1.")] // Data is converted to currency typing and must be a positive number
        public double SavingsGoal {  get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Current savings cannot be negative.")]
        public double CurrentSavings { get; set; } = 0; // // Data is converted to currency typing, default value of 0 and not a required input

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Not a valid Date.")]
        public DateTime DueDate { get; set; } // Uses DateTime method to convert user input into a date
    }
}
