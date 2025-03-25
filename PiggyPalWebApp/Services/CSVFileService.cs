using Microsoft.VisualBasic.FileIO;
using PiggyPalWebApp.Models.Database;
using System.Data;
using System.Diagnostics.Metrics;

namespace PiggyPalWebApp.Services
{
    public class CSVFileService
    {

        public CSVFileService()
        {

        }

        /// <summary>
        /// Parses given file with set delimiters and returns a collection of Record objects.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="delimiters"></param>
        /// <returns>Collection of Records or null if the file is empty.</returns>
        public ICollection<Record>? ParseFileToRecords(string filePath, string[] delimiters)
        {
            // Declare Collection of Records to a null default
            ICollection<Record>? Records = [];

            // Create and set up the parser and its delimiters
            var parser = new TextFieldParser(filePath)
            {
                TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
            };
            parser.SetDelimiters(delimiters);

            // Get first row (columns)
            string[]? dataFileColumns = parser.ReadFields();
            int[] parsedColumnIDs = [0, 0, 0];

            if (dataFileColumns != null)
            {
                int x = 0;
                foreach (var column in dataFileColumns)
                {
                    if (column.Contains("Date")) parsedColumnIDs[0] = x;
                    if (column.Contains("Amount")) parsedColumnIDs[1] = x;
                    if (column.Contains("Description")) parsedColumnIDs[2] = x;
                    x++;
                }
            }
            else return null;

            // Go through file and find rows and create Record objects
            while (!parser.EndOfData)
            {
                // Get row
                string[] row = parser.ReadFields();

                // If row isnt null add to the Records Collection
                if (row != null)
                {
                    Records.Add(new Record() { 
                        DateOfRecord = DateTime.Parse(row[parsedColumnIDs[0]]), 
                        RecordAmount = double.Parse(row[parsedColumnIDs[1]]),
                        Description = row[parsedColumnIDs[2]]

                    });
                }

            }

            // Return collected Record Objects
            return Records;
        }
    }
}
