using Microsoft.AspNetCore.Mvc;
using PiggyPalWebApp.Models.Database;
using PiggyPalWebApp.Models.ViewModels;
using PiggyPalWebApp.Services;

namespace PiggyPalWebApp.Controllers
{
    public class GoalsController : Controller
    {
        private readonly GoalService _goalService;

        // Constructor to initialize the GoalService
        public GoalsController(GoalService goalService)
        {
            _goalService = goalService;
        }

        // GET for displaying the list of goals
        public IActionResult Index()
        {
            var viewModel = new GoalViewModel
            {
                // Retrieves all goals and calculate their monthly required savings
                Goals = _goalService.GetAllGoals().Select(goal =>
                {
                    goal.CalculateMonthlySavings();
                    return goal;
                }).ToList()
            };

            // Returns the view with the populated viewModel
            return View("~/Views/Home/Goals.cshtml", viewModel);
        }

        // POST for adding a new goal
        [HttpPost]
        public IActionResult Add(GoalViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Calls the GoalService to add a new goal
                _goalService.AddGoal(viewModel.NewGoal);
                return RedirectToAction("Index");
            }

            // Grabs all the goals and returns to the view with an error message if invalid
            viewModel.Goals = _goalService.GetAllGoals();
            return View("~/Views/Home/Goals.cshtml", viewModel);
        }

        // POST for adding more savings to an existing goal
        [HttpPost]
        public IActionResult AddSavings(int id, double additionalAmount)
        {
            // Used to retrieve the goal based on the provided ID
            var goal = _goalService.GetAllGoals().FirstOrDefault(g => g.GoalId == id);

            // Checks for null goal or the additional savings amount is invalid
            if (goal == null || additionalAmount <= 0)
            {
                // Set the error message in TempData and redirect back to the goals list
                TempData["Error"] = "Invalid goal or amount.";
                return RedirectToAction("Index");
            }

            // Checks if the added savings exceeds the current goal amount
            if (goal.CurrentSavings + additionalAmount > goal.SavingsGoal)
            {
                // Set the error message in TempData
                TempData["Error"] = "Cannot exceed the savings goal.";
            }
            else
            {
                // Adds the additional savings to the current savings amount
                goal.CurrentSavings += additionalAmount;

                if (goal.CurrentSavings == goal.SavingsGoal)
                {
                    // Success message is displayed in TempData
                    TempData["Success"] = $"Goal '{goal.GoalName}' has been met!";
                }
            }

            // Recalculates the required monthly savings after adding additional savings
            goal.CalculateMonthlySavings();

            return RedirectToAction("Index");
        }

        // POST for deleting a goal
        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Calls on the GoalService to delete the goal by ID
            _goalService.DeleteGoal(id);
            return RedirectToAction("Index");
        }
    }
}
