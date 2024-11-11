using SurveyPlatform.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SurveyPlatform.Models
{
    public class SurveyAnswer
    {
        public int Id { get; set; }
        [Required]
        public string SurveyTitle { get; set; }
        public List<AnswerDetails> SurveyAnswers { get; set; }
    }
    public class AnswerDetails
    {
        public string Answer { get; set; }
        public string QuestionTitle { get; set; }
        public int QuestionType { get; set; }

    }
    public class SurveyAnswerList
    {
        public string SurveyTitle { get; set; }
        public int Total { get; set; }

    }
    public class SurveyResponseDetails
    {
        public string Title { get; set; }
        public int Count { get; set; }
        public List<AnswerDetails> SurveyAnswers { get; set; }

    }
}