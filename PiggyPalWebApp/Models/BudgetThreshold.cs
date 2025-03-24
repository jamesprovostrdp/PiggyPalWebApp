using System.ComponentModel.DataAnnotations;

namespace PiggyPalWebApp.Models
{
    public class BudgetThreshold
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public decimal LimitAmount { get; set; }

        public decimal CurrentSpending { get; set; } = 0;
    }
}