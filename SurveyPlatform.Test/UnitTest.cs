using Microsoft.VisualStudio.TestTools.UnitTesting;
using SurveyPlatform.Controllers;
using SurveyPlatform.DAL.Models;
using SurveyPlatform.Models; // Add this using directive
namespace SurveyPlatform.Test
{
    [TestClass]
    public class UnitTest
    {
        //create test method for the function SurveyAnswer/GetAllSurveyAnswers
        [TestMethod]
        public void TestAnswerGetAllSurveyAnswers()
        {
            //Arrange
            var controller = new SurveyAnswerController();

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsNotNull(result);
        }
        //create test method for the function SurveyAnswer/GetSurveyAnswersByTitle
        [TestMethod]
        public void TestAnswerGetSurveyAnswersByTitle()
        {
            //Arrange
            var controller = new SurveyAnswerController();

            //Act
            var result = controller.Details("title");

            //Assert
            Assert.IsNotNull(result);
        }
        //create test method for the function SurveyAnswer/CreateSurveyAnswer
        [TestMethod]
        public void TestAnswerCreateSurveyAnswer()
        {
            //Arrange
            var controller = new SurveyAnswerController();
            var surveyAnswer = new SurveyAnswer();
            surveyAnswer.SurveyTitle = "title test controller";
            surveyAnswer.SurveyAnswers = new List<AnswerDetails>
            {
                new AnswerDetails
                {
                    Answer = "answer test controller",
                    QuestionTitle = "question test controller",
                    QuestionType = Convert.ToInt32(QuestionType.Detail)
                }
            };
            //Act
            var result = controller.Create(surveyAnswer);

            //Assert
            Assert.IsNotNull(result);
        }
        //create test method for the function SurveyQuestion/Index
        [TestMethod]
        public void TestQuestionIndex()
        {
            //Arrange
            var controller = new SurveyQuestionController();

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsNotNull(result);
        }
        //create test method for the function SurveyQuestion/Details
        [TestMethod]
        public void TestQuestionDetails()
        {
            //Arrange
            var controller = new SurveyQuestionController();

            //Act
            var result = controller.Details(1);

            //Assert
            Assert.IsNotNull(result);
        }
        //create test method for the function SurveyQuestion/Create
        [TestMethod]
        public void TestQuestionCreate()
        {
            //Arrange
            var controller = new SurveyQuestionController();
            var surveyQuestion = new SurveyQuestion();
            surveyQuestion.Title = "title test controller";
            surveyQuestion.Questions = new List<Question>
            {
                new Question
                {
                    QuestionText = "question test controller",
                    Type = QuestionType.Detail
                }
            };
            //Act
            var result = controller.Create(surveyQuestion);

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
