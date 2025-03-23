using PiggyPalWebApp.Models;
using PiggyPalWebApp.Models.Database;
// ADD DATABASE CONNECTIVITY THANKS
namespace PiggyPalWebApp.Services
{
    public class SavingsGoalService
    {
        private readonly DatabaseContext _context;

        public SavingsGoalService(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<SavingsGoal>> GetUserGoals(int userId)
        {
            return await _context.SavingsGoals.Where(g => g.UserId == userId).ToListAsync();
        }

        public async Task<bool> AddSavingsGoal(SavingsGoal goal)
        {
            _context.SavingsGoals.Add(goal);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateSavingsGoal(int goalId, decimal amount)
        {
            var goal = await _context.SavingsGoals.FindAsync(goalId);
            if (goal == null) return false;

            goal.CurrentAmount += amount;

            if (goal.CurrentAmount >= goal.TargetAmount)
            {
                goal.Status = "Achieved";
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
