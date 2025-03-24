﻿using System.ComponentModel.DataAnnotations;

namespace PiggyPalWebApp.Models.Database
{
    public class Goal
    {
        [Key]
        public int GoalId { get; set; }

        [Required]
        [Length(1, 25, ErrorMessage = "Name must be between 1 to 25 characters.")]
        public string GoalName { get; set; } = "Goal";

        [Required]
        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Target Amount must be a positive value.")]
        public double TargetAmount { get; set; }

        public double CurrentAmount { get; set; } = 0;

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Not a valid Date.")]
        public DateTime DueDate {  get; set; }


        public int UserId { get; set; }
        public User? Owner { get; set; }
    }
}
