namespace PiggyPalWebApp.Models.ViewModels
{
    using PiggyPalWebApp.Models.Database;
    using System.Collections.Generic;

    public class GoalViewModel
    {
        public Goal NewGoal { get; set; } = new Goal
        {
            GoalName = "",
            SavingsGoal = 0,
            CurrentSavings = 0,
            DueDate = DateTime.Today
        };

        public List<Goal> Goals { get; set; } = new List<Goal>();
    }
}
