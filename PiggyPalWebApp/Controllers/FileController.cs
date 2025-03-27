﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using PiggyPalWebApp.Models;
using PiggyPalWebApp.Models.Database;
using PiggyPalWebApp.Services;
using System.Collections.Generic;
using System.Text;

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
        [Route("csv/download/{id}/{categoryName?}")]
        public FileResult DownloadCSVFile(int id, string? categoryName)
        {
            if (categoryName == null)
            {
                categoryName = "Category";
            }
            // Collect all records that are in the given category
            var records = _context.Records
                .Where(r => r.CategoryId == id)
                    .OrderByDescending(r => r.DateOfRecord)
            .ToList();

            // Parse the Records into bytes
            var bytes = _csvFileService.ParseRecordsToBytes(records);

            return File(bytes, "text/csv", $"{categoryName}Record-{DateTime.UtcNow}.csv");
        }

    }
}
