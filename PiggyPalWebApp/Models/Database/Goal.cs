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
        [Length(1, 25, ErrorMessage = "Name must be between 1 to 25 characters.")] // Name of the goal contains a character limit of 25
        public string GoalName { get; set; } = "Goal";

        [Required]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Goal amount must be a positive value.")] // Data is converted to currency typing and must be a positive number
        public double SavingsGoal {  get; set; }

        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Current savings must be a positive value.")]
        public double CurrentSavings { get; set; } = 0; // // Data is converted to currency typing, default value of 0 and not a required input

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Not a valid Date.")]
        public DateTime DueDate { get; set; } // Uses DateTime method to convert user input into a date

        // Variable to track the amount required per paycheck to meet the goal
        public double MonthlySavings => CalculateSavingsRequired();

        // Method to calculate the amount required per paycheck to meet the goal before the due date
        private double CalculateSavingsRequired()
        {
            // Check if the DueDate is in the past
            if (DueDate < DateTime.UtcNow)
            {
                return 0; // Due date is already passed, cannot calculate savings per month
            }

            // Calculate the number of months left until the due date (present or future)
            var monthsRemaining = (DueDate.Year - DateTime.UtcNow.Year) * 12 + DueDate.Month - DateTime.UtcNow.Month;

            // Ensures monthsRemaining is at least 1 to prevent errors
            if (monthsRemaining <= 0)
            {
                return 0; // No time left, cannot calculate the savings per month
            }

            // Calculate the total amount left to save
            double amountToSave = SavingsGoal - CurrentSavings;

            // If no savings are needed, return 0
            if (amountToSave <= 0)
            {
                return 0;
            }

            // Calculate the required savings per month
            return amountToSave / monthsRemaining;
        }
    }
}
