// FILE: Models/SurveyQuestion.cs
using SurveyPlatform.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurveyPlatform.Models
{
    public class SurveyQuestion
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public List<Question> Questions { get; set; }
    }
    public class Question
    {
        public string QuestionText { get; set; }
        public QuestionType Type { get; set; }
    }
}
