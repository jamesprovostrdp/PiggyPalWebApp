using PiggyPalWebApp.Models.Database;
using System.Collections.Generic;
using System.Linq;

namespace PiggyPalWebApp.Services
{
    // Manages the collection of goals in memory
    public class GoalService
    {
        // List to store the goals
        private readonly List<Goal> _goals = new();

        // Generates a unique GoalId for each goal created
        private int _nextId = 1;

        public List<Goal> GetAllGoals() => _goals;
        
        // Adds a new goal to the list and assigns it a unique Id
        public void AddGoal(Goal goal)
        {
            goal.GoalId = _nextId++;
            _goals.Add(goal);
        }

        // Deletes a goal from the list by GoalId
        public void DeleteGoal(int id)
        {
            var goal = _goals.FirstOrDefault(g => g.GoalId == id);
            if (goal != null)
            {
                _goals.Remove(goal);
            }
        }

        // Retrieves a goal by its GoalId
        public Goal? GetGoalById(int id)
        {
            return _goals.FirstOrDefault(g => g.GoalId == id);
        }

        // Updates an existing goal in the list
        public void UpdateGoal(Goal updatedGoal)
        {
            // Finds the index of the goal to be updated by GoalId
            var index = _goals.FindIndex(g => g.GoalId == updatedGoal.GoalId);
           
            if (index != -1)
            {
                _goals[index] = updatedGoal;
            }
        }
    }
}
