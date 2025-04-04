using PiggyPalWebApp.Models.Database;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace PiggyPalWebApp.Models
{
    public class MainViewModel
    {
        public List<Record> Records { get; set; } = [];
        public List<Goal> Goals { get; set; } = [];
        public List<Category> Categories { get; set; } = [ new Category(), new Category() ];
        public ChartModel Chart { get; set; } = new();

        // FOR CATEGORY
        public int CategoryId { get; set; }
        public string CategoryUserID { get; set; }
        public string CategoryDisplayName { get; set; } = "Category";
        public double? CategorySpendingLimit { get; set; } = null;
        public bool CategoryIsExpense { get; set; } = false;


        // FOR RECORD
        public int RecordId { get; set; }
        public DateTime DateOfRecord { get; set; } = DateTime.Today;
        public string? RecordDescription { get; set; }
        public double RecordAmount { get; set; }


        // FOR GOAL
        public string GoalName { get; set; } = "Goal";
        public double SavingsGoal { get; set; }
        public double GoalCurrentSavings { get; set; } = 0;
        public DateTime DueDate { get; set; }
    }
}
