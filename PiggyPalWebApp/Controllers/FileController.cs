using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using PiggyPalWebApp.Models;
using PiggyPalWebApp.Models.Database;
using PiggyPalWebApp.Services;

namespace PiggyPalWebApp.Controllers
{
    public class FileController : Controller
    {
        private CSVFileService _csvFileService {  get; set; }

        private DatabaseContext _context { get; set; }

        public FileController(CSVFileService csvFileService, DatabaseContext context)
        {
            _csvFileService = csvFileService;
            _context = context;
        }

        // Test route returns Records from test file
        [Route("csv/test")]
        public ActionResult Index()
        {
            var records = _csvFileService.ParseFileToRecords("Services/Testing/CSVParseFile.csv", [","]).ToList();

            if (records is null || records.Count == 0) {
                return NotFound();
            }

            return View(new CSVViewModel() { Records = records });
        }

        [HttpGet]
        [Route("csv/download/{id}")]
        public async Task<ActionResult> DownloadFile(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

            if (category is null || category.Records is null)
            {
                return NotFound();
            }

            Response.Clear();
            Response.Headers.Append("Content-Disposition", $"attachment; filename={category.DisplayName}.csv");
            Response.ContentType = "text/csv";

            // Write all my data
            await Response.WriteAsync("Date,Amount,Description");
            var rows = _csvFileService.ParseRecordsToStrings(category.Records.ToList());
            await Response.CompleteAsync();

            // Not sure what else to do here
            return Ok();
        }

    }
}
