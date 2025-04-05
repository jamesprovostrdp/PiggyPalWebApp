
using PiggyPalWebApp.Models.Database;

namespace PiggyPalWebApp.Models
{
    public class ChartModel
    {
        public string Type { get; set; } = "pie";
        public List<Record> Records { get; set; } = [];
        public List<string> Labels { get; set; } = [];
    }
}
