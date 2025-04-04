using PiggyPalWebApp.Models.Database;

namespace PiggyPalWebApp.Models
{
    public class MainViewModel
    {
        public List<Record> Records { get; set; } = [];
        public List<Goal> Goals { get; set; } = [];
        public List<Category> Categories { get; set; } = [ new Category(), new Category() ];
        public ChartModel Chart { get; set; } = new();
        
        public Category PlaceholderCategory = new();

        public Record PlaceholderRecord = new();

        public Goal PlaceholderGoal = new();
    }
}
