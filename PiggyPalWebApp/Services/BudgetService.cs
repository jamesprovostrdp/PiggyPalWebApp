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

        //// Get user's current budget data
        //public async Task<Budget?> GetUserBudget(int userId)
        //{
        //    return await _context.Budgets.FirstOrDefaultAsync(budget => budget.UserId == userId);
        //}

        //// Create or update the budget
        //public async Task<bool> SetUserBudget(int userId, decimal income, decimal expectedSavings)
        //{
        //    var userBudget = await _context.Budgets.FirstOrDefaultAsync(budget => budget.UserId == userId);
        //    if (userBudget == null)
        //    {
        //        userBudget = new Budget { UserId = userId, Income = income, ExpectedSavings = expectedSavings };
        //        _context.Budgets.Add(userBudget);
        //    }
        //    else
        //    {
        //        userBudget.Income = income;
        //        userBudget.ExpectedSavings = expectedSavings;
        //    }

        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        //// Get the amount left to spend
        //public async Task<decimal> GetRemainingBudget(int userId)
        //{
        //    var userBudget = await _context.Budgets.FirstOrDefaultAsync(budget => budget.UserId == userId);
        //    if (userBudget == null) return 0;

        //    // Sums up all expenses for the user
        //    var totalExpenses = await _context.Transactions
        //        .Where(transaction => transaction.UserId == userId && transaction.Type == "Expense")
        //        .SumAsync(transaction => transaction.Amount);

        //    return userBudget.AvailableBudget - totalExpenses;
        //}

        //// Check if a new expense exceeds the budget
        //public async Task<bool> IsOverBudget(int userId, decimal newExpense)
        //{
        //    var remainingBudget = await GetRemainingBudget(userId);
        //    return newExpense > remainingBudget;
        //}
    }
}
