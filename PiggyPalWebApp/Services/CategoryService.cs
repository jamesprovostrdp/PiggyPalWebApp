using System.Linq;
using PiggyPalWebApp.Models.Database;

namespace PiggyPalWebApp.Services
{
    public class CategoryService
    {
        private readonly DatabaseContext _context;

        public CategoryService(DatabaseContext context)
        {
            _context = context;
        }

        // Method to get total spending for a specific category
        public double GetTotalSpending(int categoryId)
        {
            var category = _context.Categories
                                   .FirstOrDefault(category => category.CategoryId == categoryId);

            return category?.Records?.Sum(record => record.RecordAmount) ?? 0;
        }

        // Method to check if the user is over budget in a category
        public bool IsOverBudget(int categoryId)
        {
            var category = _context.Categories
                                   .FirstOrDefault(c => c.CategoryId == categoryId);

            if (category == null || category.SpendingLimit == null)
                return false;

            var totalSpending = GetTotalSpending(categoryId);
            return totalSpending > category.SpendingLimit.Value;
        }
    }
}
