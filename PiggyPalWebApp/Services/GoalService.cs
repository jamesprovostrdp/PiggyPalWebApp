using PiggyPalWebApp.Models.Database;
using System.Collections.Generic;
using System.Linq;

namespace PiggyPalWebApp.Services
{
    public class GoalService
    {
        private readonly List<Goal> _goals = new();
        private int _nextId = 1;

        public List<Goal> GetAllGoals() => _goals;

        public void AddGoal(Goal goal)
        {
            goal.GoalId = _nextId++;
            _goals.Add(goal);
        }

        public void DeleteGoal(int id)
        {
            var goal = _goals.FirstOrDefault(g => g.GoalId == id);
            if (goal != null)
            {
                _goals.Remove(goal);
            }
        }
    }
}
