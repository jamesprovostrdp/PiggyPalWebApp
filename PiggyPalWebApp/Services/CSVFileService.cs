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

        public ICollection<Record>? ParseFileToDataTable(string filePath, string[] delimiters)
        {
            // Declair Collection of Records to a null default
            ICollection<Record>? Records = null;

            // Create and set up the parser and its delimiters
            var parser = new TextFieldParser(filePath)
            {
                TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
            };
            parser.SetDelimiters(delimiters);

            // Get first row (columns)
            string[] dataFileColumns = parser.ReadFields();
            int[] parsedColumnIDs = [0, 0, 0];

            if (dataFileColumns != null)
            {
                int x = 0;
                foreach (var column in dataFileColumns)
                {
                    if (column.Contains("Date")) parsedColumnIDs[0] = x;
                    if (column.Contains("Description")) parsedColumnIDs[1] = x;
                    if (column.Contains("Amount")) parsedColumnIDs[2] = x;
                    x++;
                }
            }
            else return Records;

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
                        Description = row[parsedColumnIDs[1]], 
                        RecordAmount = double.Parse(row[parsedColumnIDs[2]]) 
                    });
                }

            }

            // Return collected Record Objects
            return Records;
        }
    }
}
