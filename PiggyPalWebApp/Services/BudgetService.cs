using PiggyPalWebApp.Models;
using PiggyPalWebApp.Models.Database;

namespace PiggyPalWebApp.Services
{
    public class BudgetService
    {
        private readonly DatabaseContext _context;

        public BudgetService(DatabaseContext context)
        {
            _context = context;
        }

        // Get user's current budget data
        public async Task<Budget?> GetUserBudget(int userId)
        {
            return await _context.Budgets.FirstOrDefaultAsync(b => b.UserId == userId);
        }

        // Create or update the budget
        public async Task<bool> SetUserBudget(int userId, decimal income, decimal expectedSavings)
        {
            var budget = await _context.Budgets.FirstOrDefaultAsync(b => b.UserId == userId);
            if (budget == null)
            {
                budget = new Budget { UserId = userId, Income = income, ExpectedSavings = expectedSavings };
                _context.Budgets.Add(budget);
            }
            else
            {
                budget.Income = income;
                budget.ExpectedSavings = expectedSavings;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        // Get the amount left to spend
        public async Task<decimal> GetRemainingBudget(int userId)
        {
            var budget = await _context.Budgets.FirstOrDefaultAsync(b => b.UserId == userId);
            if (budget == null) return 0;

            // Sums up all expenses for the user
            var totalExpenses = await _context.Transactions
                .Where(t => t.UserId == userId && t.Type == "Expense")
                .SumAsync(t => t.Amount);

            return budget.AvailableBudget - totalExpenses;
        }

        // Check if a new expense exceeds the budget
        public async Task<bool> IsOverBudget(int userId, decimal newExpense)
        {
            var remainingBudget = await GetRemainingBudget(userId);
            return newExpense > remainingBudget;
        }
    }
}
