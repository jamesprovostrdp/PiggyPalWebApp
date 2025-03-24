using Microsoft.AspNetCore.Mvc;
using PiggyPalWebApp.Models;
using PiggyPalWebApp.Services;

namespace PiggyPalWebApp.Controllers
{
    public class FileController : Controller
    {
        private CSVFileService _csvFileService {  get; set; }

        public FileController(CSVFileService csvFileService)
        {
            _csvFileService = csvFileService;
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

    }
}
