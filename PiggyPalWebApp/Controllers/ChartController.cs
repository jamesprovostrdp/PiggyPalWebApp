using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PiggyPalWebApp.Models.Database;
using System;

namespace PiggyPalWebApp.Services
{
    public class ChartController : Controller
    {

        private readonly DatabaseContext _context;

        public ChartController(DatabaseContext context)
        {
            _context = context;
        }



        public IActionResult Main()
        {
            var records = _context.Records
                .OrderBy(r => r.DateOfRecord)
                .Select(r => new
                {
                    Date = r.DateOfRecord.ToString("dd-mm-yyyy"),
                    r.Category,
                    r.RecordAmount
                }).ToList();

            ViewBag.ChartData = System.Text.Json.JsonSerializer.Serialize(records);
            return View();
        }    
    }
}
