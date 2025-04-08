using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using PiggyPalWebApp.Models;
using PiggyPalWebApp.Models.Database;
using System.Text.Json;


namespace PiggyPalWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private DatabaseContext _context { get; set; }
    private UserManager<User> _userManager { get; set; }


    public HomeController(ILogger<HomeController> logger, DatabaseContext context, UserManager<User> userManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public IActionResult Protected()
    {
        return Ok("Authroized!");
    }

    [HttpGet]
    [Route("/main")]
    public async Task<IActionResult> Main(MainViewModel viewModel)
    {
        // Get current user, if none redirect
        User? user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Index");
        }

        // Make new viewModel if null
        if (viewModel == null)
        {
            viewModel = new MainViewModel();
        }

        // Fill categories that are related to the user
        viewModel.Categories = _context.Categories
            .Where(c => c.OwnerId == user.Id)
            .OrderBy(c => c.DisplayName)
            .ToList();

        viewModel.Records = _context.Records
            .Where(r => r.Category.OwnerId == user.Id)
            .OrderBy(r => r.DateOfRecord)
            .ToList();

        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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


    [HttpPost]
    public async Task<IActionResult> AddRecord([FromForm] DateTime DateOfRecord, [FromForm] double RecordAmount, [FromForm] int CategoryId)
    {
        User? user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Index", "Home");
        }

        Record newRecord = new()
        {
            DateOfRecord = DateOfRecord,
            RecordAmount = RecordAmount,
            CategoryId = CategoryId,

        };

        _context.Records.Add(newRecord);
        await _context.SaveChangesAsync();

        return Ok();


    }

// Record chart
    [HttpGet]
    [Route("/chart-data")]
    public async Task<IActionResult> GetChartData()
    {
        User? user = await _userManager.GetUserAsync(User);
        if (user is null)
            return Unauthorized();

        var data = _context.Records
            .Where(r => r.Category.OwnerId == user.Id)
            .Select(r => new
            {
                date = r.DateOfRecord.ToString("yyyy-MM-dd"),
                amount = r.RecordAmount,
                category = r.Category.DisplayName


            })
            .ToList();

        return Json(data);
    }

// Pie Chart
    [HttpGet]
    [Route("/chart-data/expenses-by-category")]
    public IActionResult GetExpensesByCategory()
    {
        var userId = _userManager.GetUserId(User);

        var data = _context.Records
            .Where(r => r.Category.OwnerId == userId && r.Category.IsExpense)
            .GroupBy(r => r.Category.DisplayName)
            .Select(g => new
            {
                category = g.Key,
                total = g.Sum(r => r.RecordAmount)
            })
            .ToList();

        return Json(data);
    }

//budget chart
    [HttpGet]
    [Route("/chart-data/budget-summary")]
    public async Task<IActionResult> GetBudgetSummary()
    {
        User? user = await _userManager.GetUserAsync(User);
        if (user is null) return Unauthorized();

        var result = _context.Categories
            .Where(c => c.OwnerId == user.Id && c.IsExpense && c.SpendingLimit != null)
            .Select(c => new
            {
                category = c.DisplayName,
                budget = c.SpendingLimit ?? 0,
                spent = c.Records.Sum(r => r.RecordAmount)
            })
            .ToList();

        return Json(result);
    }



    public class DeleteRecordRequest
    {
        public int RecordId { get; set; }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteRecord([FromBody] DeleteRecordRequest request)
    {
        var record = await _context.Records.FindAsync(request.RecordId);

        if (record == null)
            return NotFound();

        _context.Records.Remove(record);
        await _context.SaveChangesAsync();

        return Ok();
    }




    [HttpPost]
    public async Task<IActionResult> DeleteCategory([FromBody] int categoryId)
    {
        var category = await _context.Categories.FindAsync(categoryId);

        if (category == null)
            return NotFound();

     
        var records = _context.Records.Where(r => r.CategoryId == categoryId);
        _context.Records.RemoveRange(records);

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        return Ok();
    }






}
