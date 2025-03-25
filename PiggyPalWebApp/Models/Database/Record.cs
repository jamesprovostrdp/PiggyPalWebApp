using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PiggyPalWebApp.Models.Database
{
    public class Record
    {
        [Key]
        public int RecordId { get; set; }
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Not a valid Date.")]
        public DateTime DateOfRecord { get; set; } = DateTime.Today;

        [AllowNull]
        [MaxLength(35)]
        public string? Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a positive value.")]
        public double Amount { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
