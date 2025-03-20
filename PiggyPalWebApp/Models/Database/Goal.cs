namespace PiggyPalWebApp.Models.Database
{
    public class Goal
    {
        public int GoalId { get; set; }
        public string GoalName { get; set; }

        public double GoalAmount {  get; set; }

        public DateTime DueDate {  get; set; }
    }
}
