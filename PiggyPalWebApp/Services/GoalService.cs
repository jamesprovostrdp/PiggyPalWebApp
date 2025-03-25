using PiggyPalWebApp.Models.Database;

namespace PiggyPalWebApp.Services
{
    // Service for Goals to handle goal savings logic
    public class GoalService
    {
        // Tuple returning the savings per month required and a message to accompany it
        // CalculateMonthlySavings method grabs data from the Goal model class
        public (double amountRequired, string message) CalculateMonthlySavings(Goal goal)
        {
            // Error handling for if the goal data such as due date or savings goal are missing/null
            if (goal == null)
            {
                return (0, "Goal data is missing.");
            }

            // Check if the DueDate is in the past which is not allowed
            if (goal.DueDate < DateTime.UtcNow)
            {
                return (0, "The goal's due date has already passed. Please set a new due date.");
            }

            // Calculate the number of months left until the due date
            var monthsRemaining = (goal.DueDate.Year - DateTime.UtcNow.Year) * 12 + goal.DueDate.Month - DateTime.UtcNow.Month;

            // Ensure monthsRemaining is at least 1 to prevent errors
            if (monthsRemaining <= 0)
            {
                return (0, "The goal's due date is too close or has reached the deadline");
            }

            // Calculate the total amount left to save using the savings goal minus the initial savings
            double amountToSave = goal.SavingsGoal - goal.CurrentSavings;

            // If no savings are needed, return 0
            if (amountToSave <= 0)
            {
                return (0, "You have already met or exceeded your savings goal.");
            }

            // Calculate the required savings per month
            double amountPerMonth = amountToSave / monthsRemaining;

            // Returns the amount required to save per month to reach the goal, formatted to 2 decimal places
            return (amountPerMonth, $"You need to save ${amountPerMonth:F2} per month to reach your goal.");
        }
    }
}
