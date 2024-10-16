// FILE: Models/SurveyQuestion.cs
using System.ComponentModel.DataAnnotations;

namespace SurveyPlatform.Models
{
    public class SurveyQuestion
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Question1 { get; set; } = string.Empty;
        public string? Question2 { get; set; }
        public string? Question3 { get; set; }
        public string? Question4 { get; set; }
    }
}
