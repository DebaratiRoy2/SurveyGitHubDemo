using Newtonsoft.Json;
using SurveyPlatform.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyPlatform.DAL.Repos
{
    public class SurveyAnswerRepository
    {
        private List<SurveyAnswer> surveyAnswers;
        string _filePath = string.Empty;
        public SurveyAnswerRepository()
        {
            surveyAnswers = LoadSurveyAnswers();
        }

        private List<SurveyAnswer> LoadSurveyAnswers()
        {
            string projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            if (projectFolder.Contains("SurveyPlatform.Test"))
            {
                projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            }
            _filePath = Path.Combine(projectFolder, @"SurveyPlatform.DAL\MockData\SurveyAnswers.json");
            try
            {
                var jsonData = File.ReadAllText(_filePath);
                return JsonConvert.DeserializeObject<List<SurveyAnswer>>(jsonData) ?? new List<SurveyAnswer>();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File not found: {_filePath}");
                return new List<SurveyAnswer>();
            }
            catch (JsonException)
            {
                Console.WriteLine("Error parsing JSON data.");
                return new List<SurveyAnswer>();
            }
        }

        public List<SurveyAnswerList> GetAllSurveyAnswers()
        {
            return surveyAnswers
                .GroupBy(sa => sa.SurveyTitle)
                .Select(g => new SurveyAnswerList
                {
                    SurveyTitle = g.Key,
                    Total = g.Count()
                })
                .ToList();
        }

        public void CreateSurveyAnswer(SurveyAnswer surveyAnswer)
        {
            surveyAnswer.Id = surveyAnswers.Any() ? surveyAnswers.Max(x => x.Id) + 1 : 1;
            surveyAnswers.Add(surveyAnswer);
            SaveSurveyAnswers();
        }

        private void SaveSurveyAnswers()
        {
            try
            {
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(surveyAnswers, Formatting.Indented));
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error writing to file.");
                throw;
            }
        }

        public SurveyResponseDetails GetSurveyAnswersByTitle(string title)
        {
            var surveyResponse = new SurveyResponseDetails
            {
                Title = title,
                Count = surveyAnswers.Count(sa => sa.SurveyTitle.Equals(title, StringComparison.OrdinalIgnoreCase)),
                SurveyAnswers = surveyAnswers
                    .Where(sa => sa.SurveyTitle.Equals(title, StringComparison.OrdinalIgnoreCase))
                    .SelectMany(sa => sa.SurveyAnswers)
                    .GroupBy(ad => ad.QuestionTitle)
                    .Select(g =>
                    {
                        var questionType = g.First().QuestionType;
                        string answer;

                        if (questionType == 0)
                        {
                            var average = g.Average(ad => double.TryParse(ad.Answer, out var value) ? value : 0);
                            answer = average.ToString("F2");
                        }
                        else
                        {
                            answer = string.Join("¦ ", g.Select(ad => ad.Answer));
                        }

                        return new AnswerDetails
                        {
                            QuestionTitle = g.Key,
                            Answer = answer,
                            QuestionType = questionType
                        };
                    })
                    .ToList()
            };

            return surveyResponse;
        }
    }
}
