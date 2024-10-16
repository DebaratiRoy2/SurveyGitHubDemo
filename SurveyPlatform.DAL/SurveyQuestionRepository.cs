using System.Collections.Generic;

namespace SurveyPlatform.Data
{
    public class SurveyRepository
    {
        // This is a placeholder for your data source, e.g., a database context
        private readonly List<> _responses = new List<SurveyResponse>();

        public IEnumerable<SurveyResponse> GetAllResponses()
        {
            return _responses;
        }

        public void AddResponse(SurveyResponse response)
        {
            _responses.Add(response);
        }
    }
}
