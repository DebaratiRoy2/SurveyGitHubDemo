using SurveyPlatform.DAL;
using SurveyPlatform.DAL.Repos;
using SurveyPlatform.Models;
using System.Collections.Generic;
namespace SurveyPlatform.BAL
{
    public class SurveyAnswerCoordinator
    {
        private SurveyAnswerRepository _surveyAnswerRepo;

        public SurveyAnswerCoordinator()
        {
            _surveyAnswerRepo = new SurveyAnswerRepository();
        }
        public List<SurveyAnswerList> GetAllSurveyAnswers()
        {
            return _surveyAnswerRepo.GetAllSurveyAnswers();
        }
        public SurveyResponseDetails GetSurveyAnswersByTitle(string title)
        {
            return _surveyAnswerRepo.GetSurveyAnswersByTitle(title);
        }
        public void CreateSurveyAnswer(SurveyAnswer surveyAnswer)
        {
            _surveyAnswerRepo.CreateSurveyAnswer(surveyAnswer);
        }
    }
}
