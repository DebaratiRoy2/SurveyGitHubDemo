using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SurveyPlatform.BAL;
using SurveyPlatform.Models;

namespace SurveyPlatform.Controllers
{
    public class SurveyAnswerController : Controller
    {
        private readonly SurveyAnswerCoordinator _surveyAnswerBAL;

        public SurveyAnswerController()
        {
            _surveyAnswerBAL = new SurveyAnswerCoordinator();
        }

        [HttpGet]
        [Route("/SurveyAnswer")]
        public ActionResult<IEnumerable<SurveyAnswerList>> Index()
        {
            var answerDetails = _surveyAnswerBAL.GetAllSurveyAnswers();
            return View(answerDetails);
        }

        [HttpGet]
        public ActionResult<SurveyResponseDetails> Details(string title)
        {
            var surveyAnswer = _surveyAnswerBAL.GetSurveyAnswersByTitle(title);
            if (surveyAnswer == null)
            {
                return NotFound();
            }
            return View(surveyAnswer);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SurveyAnswer surveyAnswer)
        {
            if (surveyAnswer == null || surveyAnswer.SurveyAnswers == null || !surveyAnswer.SurveyAnswers.Any())
            {
                return BadRequest("Survey answer is null or contains no answers");
            }

            try
            {
                _surveyAnswerBAL.CreateSurveyAnswer(surveyAnswer);
                return CreatedAtAction(nameof(Details), new { title = surveyAnswer.SurveyTitle }, surveyAnswer);
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
    }
}
