using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PiggyPalWebApp.Models.Database;
using System;

namespace PiggyPalWebApp.Controllers
{
    [Route("api/goals")]
    [ApiController]
    [Authorize] // Requires authentication
    public class GoalsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public GoalsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/goals/user/{userId} - Fetch all goals for a user
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserGoals(int userId)
        {
            var goals = await _context.Goals.Where(g => g.UserId == userId).ToListAsync();
            return Ok(goals);
        }

        // POST: api/goals/add - Add a new goal
        [HttpPost("add")]
        public async Task<IActionResult> AddGoal([FromBody] Goal goal)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.Goals.Add(goal);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserGoals), new { userId = goal.UserId }, goal);
        }

        // PUT: api/goals/update/{goalId} - Update a goal
        [HttpPut("update/{goalId}")]
        public async Task<IActionResult> UpdateGoal(int goalId, [FromBody] Goal updatedGoal)
        {
            var goal = await _context.Goals.FindAsync(goalId);
            if (goal == null) return NotFound("Goal not found.");

            goal.GoalName = updatedGoal.GoalName;
            goal.SavingsGoal = updatedGoal.SavingsGoal;
            goal.CurrentSavings = updatedGoal.CurrentSavings;
            goal.DueDate = updatedGoal.DueDate;

            await _context.SaveChangesAsync();
            return Ok(goal);
        }

        // DELETE: api/goals/delete/{goalId} - Delete a goal
        [HttpDelete("delete/{goalId}")]
        public async Task<IActionResult> DeleteGoal(int goalId)
        {
            var goal = await _context.Goals.FindAsync(goalId);
            if (goal == null) return NotFound("Goal not found.");

            _context.Goals.Remove(goal);
            await _context.SaveChangesAsync();
            return Ok("Goal deleted.");
        }
    }
}
