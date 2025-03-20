using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace PiggyPalWebApp.Models.Database
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [Length(1, 25, ErrorMessage = "Name must be between 1 to 25 characters.")]
        public string DisplayName { get; set; } = "Category";

        [AllowNull]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Spending Limit must be a positive value.")]
        public decimal? SpendingLimit { get; set; } = null;

        [Required]
        public bool IsExpense { get; set; } = true;

        [Required]
        public string BackgroundColor { get; set; } = Color.White.ToString();

        public int UserID { get; set; }
        public User? Owner { get; set; }

        public ICollection<Record>? Records { get; set; }

    }
}
