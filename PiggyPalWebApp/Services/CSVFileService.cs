using Microsoft.VisualBasic.FileIO;
using System.Data;

namespace PiggyPalWebApp.Services
{
    public class CSVFileService
    {

        public CSVFileService()
        {

        }

        public async Task<DataTable> ParseFileToDataTable(string filePath, string[] delimiters)
        {
            // Create and set up the parser and its delimiters
            var parser = new TextFieldParser(filePath)
            {
                TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited
            };
            parser.SetDelimiters(delimiters);

            string[] row = parser.ReadFields();

            while (!parser.EndOfData)
            {
            }
        }
    }
}
