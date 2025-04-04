using Microsoft.AspNetCore.Mvc;
using PiggyPalWebApp.Models;

namespace PiggyPalWebApp.Controllers
{
    public class CategoryController : Controller
    {
        [HttpPost]
        public IActionResult CreateIncomeCategory(MainViewModel viewModel)
        {
            viewModel.Categories.Add(viewModel.PlaceholderCategory);

            return RedirectToAction("Main", "Home", viewModel);
        }
    }
}
