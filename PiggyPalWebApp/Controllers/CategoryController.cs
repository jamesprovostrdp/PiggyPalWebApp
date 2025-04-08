using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PiggyPalWebApp.Models;
using PiggyPalWebApp.Models.Database;

namespace PiggyPalWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private DatabaseContext _context { get; set; }
        private UserManager<User> _userManager { get; set; }

        public CategoryController(DatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        [HttpPost]
        public async Task<IActionResult> CreateIncomeCategory(MainViewModel viewModel)
        {
            User? user = await _userManager.GetUserAsync(User);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }

            Category newCategory = new()
            {
                DisplayName = viewModel.CategoryDisplayName,
                OwnerId = user.Id,
                SpendingLimit = viewModel.CategorySpendingLimit,
                IsExpense = viewModel.CategoryIsExpense
            };

            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction("Main", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> CreateExpenseCategory(MainViewModel viewModel)
        {
            User? user = await _userManager.GetUserAsync(User);
            if (user is null)
            {
                return RedirectToAction("Index", "Home");
            }

            Category newCategory = new()
            {
                DisplayName = viewModel.CategoryDisplayName,
                OwnerId = user.Id,
                SpendingLimit = viewModel.CategorySpendingLimit,
                IsExpense = viewModel.CategoryIsExpense
            };

            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction("Main", "Home");
        }
    }
}
