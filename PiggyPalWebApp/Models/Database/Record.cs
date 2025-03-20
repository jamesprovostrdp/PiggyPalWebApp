using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace PiggyPalWebApp.Models.Database
{
    public class Record
    {
        [Key]
        public int RecordId { get; set; }
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Not a valid Date.")]
        public DateTime DateOfRecord { get; set; } = DateTime.Today;
        [Required]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive value.")]
        public double RecordAmount { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
