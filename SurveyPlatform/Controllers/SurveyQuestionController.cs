using Microsoft.AspNetCore.Mvc;
using SurveyPlatform.BAL;
using SurveyPlatform.Models;

namespace SurveyPlatform.Controllers
{
    public class SurveyQuestionController : Controller
    {
        private readonly SurveyQuestionCoordinator _surveyQuestionBAL;

        public SurveyQuestionController()
        {
            _surveyQuestionBAL = new SurveyQuestionCoordinator();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult<IEnumerable<SurveyQuestion>> Index()
        {
            var surveyQuestions = _surveyQuestionBAL.GetAllSurveyQuestions();
            return View(surveyQuestions);
        }

        [HttpGet]
        public ActionResult<SurveyQuestion> Details(int id)
        {
            var surveyQuestion = _surveyQuestionBAL.GetSurveyQuestionById(id);
            if (surveyQuestion == null)
            {
                return NotFound();
            }
            return View(surveyQuestion);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SurveyQuestion surveyQuestion)
        {
            if (surveyQuestion == null)
            {
                return BadRequest("Survey question is null.");
            }

            try
            {
                _surveyQuestionBAL.CreateSurveyQuestion(surveyQuestion);
                return CreatedAtAction(nameof(Details), new { id = surveyQuestion.Id }, surveyQuestion);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }
        [HttpGet]
        public bool GetSurveyDetailByTitle(string title)
        {
            int availableId = _surveyQuestionBAL.GetSurveyDetailByTitle(title)?.Id ?? 0;
            bool isAvailable = availableId > 0;
            return isAvailable;
        }
    }
}
