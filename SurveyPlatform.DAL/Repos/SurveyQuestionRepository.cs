using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SurveyPlatform.Models;

namespace SurveyPlatform.DAL
{
    public class SurveyQuestionRepository
    {
        string _filePath = string.Empty;
        private List<SurveyQuestion> surveyQuestions;
        public SurveyQuestionRepository()
        {
            surveyQuestions = LoadSurveyQuestions();
        }

        private List<SurveyQuestion> LoadSurveyQuestions()
        {
            string projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            if (projectFolder.Contains("SurveyPlatform.Test"))
            {
                projectFolder = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            }

            _filePath = Path.Combine(projectFolder, @"SurveyPlatform.DAL\MockData\SurveyQuestions.json");
            try
            {
                var jsonData = File.ReadAllText(_filePath);
                return JsonConvert.DeserializeObject<List<SurveyQuestion>>(jsonData) ?? new List<SurveyQuestion>();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File not found: {_filePath}");
                return new List<SurveyQuestion>();
            }
            catch (JsonException)
            {
                Console.WriteLine("Error parsing JSON data.");
                return new List<SurveyQuestion>();
            }
        }

        public IEnumerable<SurveyQuestion> GetAll()
        {
            return surveyQuestions;
        }

        public SurveyQuestion GetById(int id)
        {
            return surveyQuestions.Find(sq => sq.Id == id);
        }
        public SurveyQuestion GetByTitle(string title)
        {
            return surveyQuestions.Find(sq => sq.Title == title);
        }

        public void Create(SurveyQuestion surveyQuestion)
        {
            surveyQuestion.Id = surveyQuestions.Any() ? surveyQuestions.Max(x => x.Id) + 1 : 1;
            surveyQuestions.Add(surveyQuestion);
            SaveSurveyQuestions();
        }

        private void SaveSurveyQuestions()
        {
            try
            {
                File.WriteAllText(_filePath, JsonConvert.SerializeObject(surveyQuestions, Formatting.Indented));
            }
            catch (IOException)
            {
                Console.WriteLine("Error writing to file.");
                throw;
            }
        }
    }
}
