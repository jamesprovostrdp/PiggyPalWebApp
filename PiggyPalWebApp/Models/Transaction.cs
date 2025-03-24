using System.ComponentModel.DataAnnotations; // Uses DataAnnotations such as Key and Required

namespace PiggyPalWebApp.Models
{
    // Transaction class for users income or expenses, used for budget calculations
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public decimal Amount { get; set; } // Payment amount

        [Required]
        public string TypeOfPayment { get; set; }  // "Income" or "Expense"

        [Required]
        public DateTime Date { get; set; } // Date of transaction
    }
}