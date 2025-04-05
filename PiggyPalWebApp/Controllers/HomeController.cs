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


    public class RecordInput
    {
        public int CategoryId { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public int SpendingLimit { get; set; }
    }


    [HttpPost]
    public async Task<IActionResult> AddRecord(MainViewModel viewModel)
    {
        User? user = await _userManager.GetUserAsync(User);
        if (user is null)
        {
            return RedirectToAction("Index", "Home");
        }

        Record newRecord = new()
        {
            DateOfRecord = viewModel.DateOfRecord,
            RecordAmount = viewModel.RecordAmount,
            CategoryId = viewModel.CategoryId,

        };

        _context.Records.Add(newRecord);
        await _context.SaveChangesAsync();

        return RedirectToAction("Main", "Home");


    }

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




}
