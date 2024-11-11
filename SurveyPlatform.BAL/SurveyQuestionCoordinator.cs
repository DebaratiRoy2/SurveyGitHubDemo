using SurveyPlatform.DAL;
using SurveyPlatform.Models;
using System.Collections.Generic;
namespace SurveyPlatform.BAL
{
    public class SurveyQuestionCoordinator
    {
        private SurveyQuestionRepository _surveyQuestionRepo;

        public SurveyQuestionCoordinator()
        {
            _surveyQuestionRepo = new SurveyQuestionRepository();
        }

        public IEnumerable<SurveyQuestion> GetAllSurveyQuestions()
        {
            return _surveyQuestionRepo.GetAll();
        }

        public SurveyQuestion GetSurveyQuestionById(int id)
        {
            return _surveyQuestionRepo.GetById(id);
        }
        public SurveyQuestion GetSurveyDetailByTitle(string title)
        {
            return _surveyQuestionRepo.GetByTitle(title);
        }

        public void CreateSurveyQuestion(SurveyQuestion surveyQuestion)
        {
            _surveyQuestionRepo.Create(surveyQuestion);
        }
    }
}
