using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http; // Import necessary namespace for IFormFile

namespace HealthAdviceGroup.Models
{
    // Model used to define and add constraints / conditions for data to be stored
    public class Advice
    {
        // Primary key for the advice entry
        public int Id { get; set; }

        // Property for uploading an image associated with the advice (not saved to the database)
        [NotMapped]
        [DisplayName("Upload Image")]
        public IFormFile? ImageFile { get; set; }

        // Path to the stored image in the server
        public string? ImagePath { get; set; }

        // Nullable property representing the temperature associated with the advice
        [AllowNull]
        [Range(-90, 60, ErrorMessage = "Temperature must be between -90 and 60 degrees Celsius")]
        [DisplayName("Temperature (°C)")]
        public double? Temperature { get; set; }

        // Required property representing the title of the advice which will be shown on the all advice page
        [StringLength(100, ErrorMessage = "Title must be between 5 and 100 characters", MinimumLength = 5)]
        [Required]
        public string Title { get; set; }

        // Property representing the description of the advice which will be shown on the advice details page
        public string Description { get; set; }
    }
}
