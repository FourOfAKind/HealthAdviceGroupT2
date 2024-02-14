﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthAdviceGroup.Models
{
    // Model representing health-related data entries
    public class Health
    {
        // Primary key for the health data entry, automatically generated by the database
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // User ID associated with the health data entry, generated automatically.
        [Required]
        public string UserId { get; set; }

        // Date of the health data entry, generated on the server's side.
        [Required]
        public string Date { get; set; }

        // Number of steps taken, required and must be between 0 and 200,000
        [Required]
        [Range(0, 200000, ErrorMessage = "Number must be between 0 and 200,000")]
        public int Steps { get; set; }

        // Number of calories burned, required and must be between 0 and 50,000
        [Required]
        [Range(0, 50000, ErrorMessage = "Number must be between 0 and 50,000")]
        public int Calories { get; set; }

        // Amount of water consumed in cups, required and must be between 0 and 100
        [Required]
        [Range(0, 100, ErrorMessage = "Number must be between 0 and 100.")]
        [DisplayName("Cups of Water")]
        public int Water { get; set; }
    }
}