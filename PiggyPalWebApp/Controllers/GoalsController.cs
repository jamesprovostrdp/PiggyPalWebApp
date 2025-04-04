using Microsoft.AspNetCore.Mvc;
using PiggyPalWebApp.Models.Database;
using PiggyPalWebApp.Models.ViewModels;
using PiggyPalWebApp.Services;

namespace PiggyPalWebApp.Controllers
{
    public class GoalsController : Controller
    {
        private readonly GoalService _goalService;

        public GoalsController(GoalService goalService)
        {
            _goalService = goalService;
        }

        public IActionResult Index()
        {
            var viewModel = new GoalViewModel
            {
                Goals = _goalService.GetAllGoals()
            };
            return View("~/Views/Home/Goals.cshtml", viewModel);
        }

        [HttpPost]
        public IActionResult Add(GoalViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _goalService.AddGoal(viewModel.NewGoal);
                return RedirectToAction("Index");
            }

            viewModel.Goals = _goalService.GetAllGoals();
            return View("~/Views/Home/Goals.cshtml", viewModel);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _goalService.DeleteGoal(id);
            return RedirectToAction("Index");
        }
    }
}

//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using PiggyPalWebApp.Models.Database;
//using System;

//namespace PiggyPalWebApp.Controllers
//{
//    [Route("api/goals")]
//    [ApiController]
//    [Authorize] // Requires authentication
//    public class GoalsController : ControllerBase
//    {
//        private readonly DatabaseContext _context;

//        public GoalsController(DatabaseContext context)
//        {
//            _context = context;
//        }

//        // GET: api/goals/user/{userId} - Fetch all goals for a user
//        [HttpGet("user/{userId}")]
//        public async Task<IActionResult> GetUserGoals(int userId)
//        {
//            var goals = await _context.Goals.Where(g => g.UserId == userId).ToListAsync();
//            return Ok(goals);
//        }

//        // POST: api/goals/add - Add a new goal
//        [HttpPost("add")]
//        public async Task<IActionResult> AddGoal([FromBody] Goal goal)
//        {
//            if (!ModelState.IsValid) return BadRequest(ModelState);

//            _context.Goals.Add(goal);
//            await _context.SaveChangesAsync();
//            return CreatedAtAction(nameof(GetUserGoals), new { userId = goal.UserId }, goal);
//        }

//        // PUT: api/goals/update/{goalId} - Update a goal
//        [HttpPut("update/{goalId}")]
//        public async Task<IActionResult> UpdateGoal(int goalId, [FromBody] Goal updatedGoal)
//        {
//            var goal = await _context.Goals.FindAsync(goalId);
//            if (goal == null) return NotFound("Goal not found.");

//            goal.GoalName = updatedGoal.GoalName;
//            goal.SavingsGoal = updatedGoal.SavingsGoal;
//            goal.CurrentSavings = updatedGoal.CurrentSavings;
//            goal.DueDate = updatedGoal.DueDate;

//            await _context.SaveChangesAsync();
//            return Ok(goal);
//        }

//        // DELETE: api/goals/delete/{goalId} - Delete a goal
//        [HttpDelete("delete/{goalId}")]
//        public async Task<IActionResult> DeleteGoal(int goalId)
//        {
//            var goal = await _context.Goals.FindAsync(goalId);
//            if (goal == null) return NotFound("Goal not found.");

//            _context.Goals.Remove(goal);
//            await _context.SaveChangesAsync();
//            return Ok("Goal deleted.");
//        }
//    }
//}
