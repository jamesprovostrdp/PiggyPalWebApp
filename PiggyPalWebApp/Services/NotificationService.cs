using PiggyPalWebApp.Models;
using PiggyPalWebApp.Models.Database;
// ADD DATABASE CONNECTIVITY THANKS
namespace PiggyPalWebApp.Services
{
    public class NotificationService
    {
        private readonly DatabaseContext _context;

        public NotificationService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> CheckOverspending(int userId, int categoryId, decimal newExpense)
        {
            var threshold = await _context.BudgetThresholds
                .FirstOrDefaultAsync(b => b.UserId == userId && b.CategoryId == categoryId);

            if (threshold == null) return false;

            threshold.CurrentSpending += newExpense;
            await _context.SaveChangesAsync();

            if (threshold.CurrentSpending > threshold.LimitAmount)
            {
                // Trigger notification (e.g., store in DB or send an alert)
                Console.WriteLine($"Warning: You have exceeded your budget in category {categoryId}.");
                return true;
            }

            return false;
        }
    }
}